using AutoMapper;
using NovaApp.Models;
using NovaApp.DTOs;
using NovaApp.Repositories;

namespace NovaApp.Services;

public interface IProductService
{
    Task<ProductDto> CreateAsync(CreateProductDto dto);
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(int id);
    Task<ProductDto?> UpdateAsync(int id, CreateProductDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<ProductDto>> SearchAsync(string keyword);
    Task<(IEnumerable<ProductDto> Items, int Total)> GetPagedAsync(int page, int size);
    Task<IEnumerable<ProductDto>> BulkCreateAsync(IEnumerable<CreateProductDto> dtos);
    Task<(IEnumerable<ProductDto> Items, int Total)> GetFilteredAsync(
    string? q,
    decimal? minPrice,
    decimal? maxPrice,
    double? minRating,
    string? sortBy,
    bool desc,
    int page,
    int size);

}
