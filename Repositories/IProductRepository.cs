using Microsoft.OpenApi.Any;
using NovaApp.Models;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;


namespace NovaApp.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> SearchAsync(string keyword);

    // Task BulkAddAsync(IEnumerable<Product> products);
    Task<(IEnumerable<Product> Items, int Total)> GetFilteredAsync(
        string? q,
        decimal? minPrice,
        decimal? maxPrice,
        double? minRating,
        string? sortBy,
        bool desc,
        int page,
        int size);
}