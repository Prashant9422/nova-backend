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
