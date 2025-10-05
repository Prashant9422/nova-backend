
using System.Collections.Generic;
using System.Threading.Tasks;
using NovaApp.Models;
using NovaApp.Repositories;
using System.Linq;
using NovaApp.Data;
using Microsoft.EntityFrameworkCore;

namespace NovaApp.Repositories;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    private readonly ApplicationDbContext _context;

    public ServiceRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<(IEnumerable<Service> Items, int Total)> GetFilteredAsync(string? q, decimal? minPrice, decimal? maxPrice, string? sortBy, bool desc, int page, int size)
    {
        if (page < 0) page = 0;
        if (size <= 0) size = 10;

        IQueryable<Service> query = _context.Services.AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var qq = q.Trim();
            query = query.Where(s => (s.ServiceHeading ?? "").Contains(qq) || (s.Description ?? "").Contains(qq));
        }

        if (minPrice.HasValue) query = query.Where(s => s.Pricing >= minPrice.Value);
        if (maxPrice.HasValue) query = query.Where(s => s.Pricing <= maxPrice.Value);

        var total = await query.CountAsync();

        switch (sortBy?.ToLowerInvariant())
        {
            case "pricing":
            case "price":
                query = desc ? query.OrderByDescending(s => s.Pricing) : query.OrderBy(s => s.Pricing);
                break;
            case "deliverydays":
            case "delivery":
                query = desc ? query.OrderByDescending(s => s.DeliveryDays) : query.OrderBy(s => s.DeliveryDays);
                break;
            case "heading":
            case "serviceheading":
                query = desc ? query.OrderByDescending(s => s.ServiceHeading) : query.OrderBy(s => s.ServiceHeading);
                break;
            default:
                query = desc ? query.OrderByDescending(s => s.Id) : query.OrderBy(s => s.Id);
                break;
        }

        var items = await query.Skip(page * size).Take(size).ToListAsync();
        return (items, total);
    }

    public async Task<IEnumerable<Service>> SearchAsync(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return Enumerable.Empty<Service>();
        }

        return await _context.Services
            .Where(x => (x.ServiceHeading ?? "").Contains(keyword) || (x.Description ?? "").Contains(keyword))
            .ToListAsync();
    }
}

