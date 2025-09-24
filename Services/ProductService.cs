using AutoMapper;
using NovaApp.DTOs;
using NovaApp.Models;
using NovaApp.Repositories;

namespace NovaApp.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;
    public ProductService(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> BulkCreateAsync(IEnumerable<CreateProductDto> dtos)
    {
        if (dtos == null) return Enumerable.Empty<ProductDto>();
        var entities = dtos.Select(dto => _mapper.Map<Product>(dto)).ToList();
        var created= await _repo.AddRangeAsync(entities);
        return _mapper.Map<IEnumerable<ProductDto>>(created);
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var entity = _mapper.Map<Product>(dto);
        await _repo.AddAsync(entity);
        return _mapper.Map<ProductDto>(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
        {
            return;
        }
         await _repo.DeleteAsync(entity);
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(list);
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var entities = await _repo.GetByIdAsync(id);
        if (entities == null) return null;
        return _mapper.Map<ProductDto>(entities);
    }

    public async Task<(IEnumerable<ProductDto> Items, int Total)> GetPagedAsync(int page, int size)
    { 
        var all = (await _repo.GetAllAsync()).OrderBy(p => p.Id).ToList();
        var total = all.Count;
        var items = all.Skip((page-1)*size).Take(size);
        return (_mapper.Map<IEnumerable<ProductDto>>(items),total);
    }

    public async Task<IEnumerable<ProductDto>> SearchAsync(string keyword)
    {
        var list = await _repo.SearchAsync(keyword);
        return _mapper.Map<IEnumerable<ProductDto>>(list);
    }

    public async Task<ProductDto?> UpdateAsync(int id, CreateProductDto dto)
    {
        var pro = await _repo.GetByIdAsync(id);
        if(pro==null) return null;
        _mapper.Map(dto,pro);
        await _repo.UpdateAsync(pro);
        return _mapper.Map<ProductDto>(pro);
    }
}
