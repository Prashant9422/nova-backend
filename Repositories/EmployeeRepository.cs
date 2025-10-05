using Microsoft.EntityFrameworkCore;
using NovaApp.Data;
using NovaApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaApp.Repositories;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    private readonly ApplicationDbContext _context;
    public EmployeeRepository(ApplicationDbContext context) : base(context) { _context = context; }

    public async Task<IEnumerable<Employee>> GetCoreTeamAsync()
    {
        return await _context.Employees.Where(e => e.CoreTeam).ToListAsync();
    }

    public async Task<(IEnumerable<Employee> Items, int Total)> GetFilteredAsync(
        string? q,
        string? department,
        string? position,
        bool? coreTeam,
        string? sortBy,
        bool desc,
        int page,
        int size)
    {
        if (page < 0) page = 0;
        if (size <= 0) size = 10;

        IQueryable<Employee> query = _context.Employees.AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var qq = q.Trim();
            query = query.Where(e => (e.FullName ?? "").Contains(qq) || (e.Email ?? "").Contains(qq));
        }

        if (!string.IsNullOrWhiteSpace(department)) query = query.Where(e => e.Department == department);
        if (!string.IsNullOrWhiteSpace(position)) query = query.Where(e => e.Position == position);
        if (coreTeam.HasValue) query = query.Where(e => e.CoreTeam == coreTeam.Value);

        var total = await query.CountAsync();

        switch (sortBy?.ToLowerInvariant())
        {
            case "fullname":
            case "name":
                query = desc ? query.OrderByDescending(e => e.FullName) : query.OrderBy(e => e.FullName);
                break;
            case "email":
                query = desc ? query.OrderByDescending(e => e.Email) : query.OrderBy(e => e.Email);
                break;
            case "position":
                query = desc ? query.OrderByDescending(e => e.Position) : query.OrderBy(e => e.Position);
                break;
            default:
                query = desc ? query.OrderByDescending(e => e.Id) : query.OrderBy(e => e.Id);
                break;
        }

        var items = await query.Skip(page * size).Take(size).ToListAsync();
        return (items, total);
    }


    public async Task<IEnumerable<Employee>> SearchAsync(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return Enumerable.Empty<Employee>();
        }

        return await _context.Employees
            .Where(x => (x.FullName ?? "").Contains(keyword) || (x.Email ?? "").Contains(keyword) || (x.Position ?? "").Contains(keyword))
           .ToListAsync();
    }
}
