using AutoMapper;
using NovaApp.Models;
using NovaApp.DTOs;
using NovaApp.Repositories;
namespace NovaApp.Services;

public interface IServiceService
{
    Task<CreateServiceDto> CreateAsync(CreateServiceDto dto);
    Task<IEnumerable<ServiceDto>> GetAllAsync();
    Task<ServiceDto?> GetByIdAsync(int id);
    Task<ServiceDto?> UpdateAsync(int id, CreateServiceDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<ServiceDto>> SearchAsync(string keyword);
    Task<IEnumerable<ServiceDto>> BulkCreateAsync(IEnumerable<CreateServiceDto> dtos);

    Task<(IEnumerable<ServiceDto> Items, int Total)> GetFilteredAsync(
    string? q,
    decimal? minPrice,
    decimal? maxPrice,
    string? sortBy,
    bool desc,
    int page,
    int size);

}