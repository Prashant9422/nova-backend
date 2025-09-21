using NovaApp.Models;
using NovaApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NovaApp.Repositories;

public interface IServiceRepository : IGenericRepository<Service>
{
    // add service-specific methods if needed
    Task<IEnumerable<Service>> SearchAsync(string keyword);

}
