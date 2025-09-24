using System.Collections.Generic;
using System.Threading.Tasks;
using NovaApp.Models;
using NovaApp.Repositories;
using System.Linq;
using NovaApp.Data;
using Microsoft.EntityFrameworkCore;

namespace NovaApp.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
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
