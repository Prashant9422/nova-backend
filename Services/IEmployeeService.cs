using AutoMapper;
using NovaApp.Models;
using NovaApp.DTOs;
using NovaApp.Repositories;
namespace NovaApp.Services;

public interface IEmployeeService
{
    Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto);
    Task<IEnumerable<EmployeeDto>> GetAllAsync();
    Task<EmployeeDto?> GetByIdAsync(int id);
    Task<EmployeeDto?> UpdateAsync(int id, CreateEmployeeDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<EmployeeDto>> GetCoreTeamAsync();
    Task<IEnumerable<EmployeeDto>> SearchAsync(string keyword);

    Task<IEnumerable<EmployeeDto>> BulkCreateAsync(IEnumerable<CreateEmployeeDto> dtos);
}
