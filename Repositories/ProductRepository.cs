using System.Collections.Generic;
using System.Threading.Tasks;
using NovaApp.Models;
using NovaApp.Repositories;
using System.Linq;
using NovaApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace NovaApp.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<(IEnumerable<Product> Items, int Total)> GetFilteredAsync(string? q, decimal? minPrice, decimal? maxPrice, double? minRating, string? sortBy, bool desc, int page, int size)
    {
        if (page <= 0) page = 1;
        if (size <= 0) size = 10;

        IQueryable<Product> query = _context.Products.AsQueryable();

        //Filtering
        if (!string.IsNullOrWhiteSpace(q))
        {
            var qq = q.Trim();
            query = query.Where(p => (p.ProductHeading ?? "").Contains(q) || (p.Description ?? "").Contains(q));
        }

        //Sorting
        if (minPrice.HasValue) query = query.Where(p => p.Price >= minPrice.Value);
        if (maxPrice.HasValue) query = query.Where(p => p.Price <= maxPrice.Value);
        if (minRating.HasValue) query = query.Where(p => p.Rating >= minRating.Value);

        // Count before pagination
        int total = await query.CountAsync();

        //Sorting
        //Default: sort by ID ascending
        switch (sortBy?.ToLowerInvariant())
        {
            case "price":
                query = desc ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);
                break;

            case "rating":
                query = desc ? query.OrderByDescending(p => p.Rating) : query.OrderBy(p => p.Rating);
                break;

            case "subscribercount":
            case "subscribers":
                query = desc ? query.OrderByDescending(p => p.SubscriberCount) : query.OrderBy(p => p.SubscriberCount);
                break;

            case "heading":
            case "productheading":
                query = desc ? query.OrderByDescending(p => p.ProductHeading) : query.OrderBy(p => p.ProductHeading);
                break;

            default:
                query = desc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id);
                break;
        }

        //Pagination
        var items = await query.Skip(page * size).Take(size).ToListAsync();
        return (items, total);


    }

    // Fixed the typo 'SaerchAsync' to 'SearchAsync' and implemented the method as defined in IProductRepository
    public async Task<IEnumerable<Product>> SearchAsync(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return Enumerable.Empty<Product>();
        }

        return await _context.Products
            .Where(x => (x.ProductHeading ?? "").Contains(keyword) || (x.Description ?? "").Contains(keyword))
            .ToListAsync();
    }



    // public async Task BulkAddAsync(IEnumerable<Product> products)
    // {
    //     await _context.Products.AddRangeAsync(products);
    //     await _context.SaveChangesAsync();
    // }



}
