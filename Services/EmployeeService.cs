using AutoMapper;
using NovaApp.Models;
using NovaApp.DTOs;
using NovaApp.Repositories;

namespace NovaApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
        {
            var emp = _mapper.Map<Employee>(dto);
            await _repo.AddAsync(emp);
            return _mapper.Map<EmployeeDto>(emp); // Fixed typo here (_mapper instead of _mapeer)
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(list); // Fixed typo here (_mapper instead of _mapeer)
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var emp = await _repo.GetByIdAsync(id);
            if (emp == null) return null;
            return _mapper.Map<EmployeeDto>(emp);
        }

        public async Task<EmployeeDto?> UpdateAsync(int id, CreateEmployeeDto dto)
        {
            var emp = await _repo.GetByIdAsync(id);
            if (emp == null) return null;
            _mapper.Map(dto, emp);
            await _repo.UpdateAsync(emp); // Pass 'emp' to UpdateAsync
            return _mapper.Map<EmployeeDto>(emp);
        }

        public async Task DeleteAsync(int id)
        {
            var emp = await _repo.GetByIdAsync(id); // Pass 'id' to GetByIdAsync
            if (emp == null) return;
            await _repo.DeleteAsync(emp);
        }

        public async Task<IEnumerable<EmployeeDto>> GetCoreTeamAsync()
        {
            var list = await _repo.GetCoreTeamAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(list); // Fixed typo here (_mapper instead of _mapeer)
        }

        public async Task<IEnumerable<EmployeeDto>> SearchAsync(string keyword)
        {
            var list = await _repo.SearchAsync(keyword); // Added 'await' here
            return _mapper.Map<IEnumerable<EmployeeDto>>(list);
        }
    }
}
