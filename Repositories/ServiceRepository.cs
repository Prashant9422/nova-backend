
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

