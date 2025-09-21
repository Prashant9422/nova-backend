using AutoMapper;
using NovaApp.Models;
using NovaApp.DTOs;
using NovaApp.Repositories;

namespace NovaApp.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _repo;
    private readonly IMapper _mapper;
    public ServiceService(IServiceRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<CreateServiceDto> CreateAsync(CreateServiceDto dto)
    {
        var ser = _mapper.Map<Service>(dto);
        await _repo.AddAsync(ser);
        return _mapper.Map<CreateServiceDto>(ser);
    }

    public async Task<IEnumerable<ServiceDto>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<ServiceDto>>(list);
    }

    public async Task<ServiceDto?> GetByIdAsync(int id)
    {
        var ser = await _repo.GetByIdAsync(id);
        if(ser == null) return null;
        return _mapper.Map<ServiceDto>(ser);
    }

    public async Task<ServiceDto?> UpdateAsync(int id, CreateServiceDto dto)
    {
        var ser = await _repo.GetByIdAsync(id);
        if(ser == null) return null;
        _mapper.Map(dto,ser);
        await _repo.UpdateAsync(ser);
        return _mapper.Map<ServiceDto>(ser);
    }

    public async Task DeleteAsync(int id)
    {
        var ser = await _repo.GetByIdAsync(id);
        if(ser == null) return;
        await _repo.DeleteAsync(ser);
    }

    public async Task<IEnumerable<ServiceDto>> SearchAsync(string keyword)
    {
        var list = await _repo.SearchAsync(keyword);
        return _mapper.Map<IEnumerable<ServiceDto>>(list);
    }

    

}