using NovaApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace NovaApp.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> SearchAsync(string keyword);

    // Task BulkAddAsync(IEnumerable<Product> products);
}