using NovaApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NovaApp.Repositories;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    Task<IEnumerable<Employee>> GetCoreTeamAsync();
    Task<IEnumerable<Employee>> SearchAsync(string keyword);
}
