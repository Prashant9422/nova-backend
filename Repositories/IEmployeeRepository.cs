using NovaApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NovaApp.Repositories;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    Task<IEnumerable<Employee>> GetCoreTeamAsync();
    Task<IEnumerable<Employee>> SearchAsync(string keyword);
    Task<(IEnumerable<Employee> Items, int Total)> GetFilteredAsync(
       string? q,
       string? department,
       string? position,
       bool? coreTeam,
       string? sortBy,
       bool desc,
       int page,
       int size);
}
