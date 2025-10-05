using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovaApp.Data;
using NovaApp.DTOs;
using NovaApp.Models;
using NovaApp.Repositories;
using NovaApp.Services;

namespace NovaApp.Controllers;

/*
[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public ProductsController(ApplicationDbContext db) => _db = db;

    // private readonly IProductRepository _repo;
    // public ProductsController(IProductRepository repo) => _repo = repo;


    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        var pro = new Product
        {
            ProductHeading = dto.ProductHeading,
            Description = dto.Description,
            Price = dto.Price,
            Rating = dto.Rating,
            SubscriberCount = dto.SubscriberCount,
            ImageUrl = dto.ImageUrl
        };

        await _db.Products.AddAsync(pro);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = pro.Id }, pro);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _db.Products.ToListAsync());
    }


    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var p = await _db.Products.FindAsync(id);
        if (p == null) return NotFound();
        return Ok(p);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateProductDto dto)
    {
        var pro = await _db.Products.FindAsync(id);
        if (pro == null) return NotFound();

        pro.ProductHeading = dto.ProductHeading;
        pro.Description = dto.Description;
        pro.Price = dto.Price;
        pro.Rating = dto.Rating;
        pro.SubscriberCount = dto.SubscriberCount;
        pro.ImageUrl = dto.ImageUrl;

        await _db.SaveChangesAsync();
        return Ok(pro);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var pro = await _db.Products.FindAsync(id);
        if (pro == null) return NotFound();

        _db.Products.Remove(pro);
        await _db.SaveChangesAsync();
        return NoContent();
    }


    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? q)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return BadRequest("Provide search query");
        }

        var items = await _db.Products
         .Where(x => (x.ProductHeading ?? "").Contains(q) || (x.Description ?? "").Contains(q))
         .ToListAsync();

        return Ok(items);
    }

    [HttpGet("page")]
    public async Task<IActionResult> Page([FromQuery] int page = 0, [FromQuery] int size = 10)
    {
        if (page < 0) page = 0;
        if (size < 10) size = 10;

        var total = await _db.Products.CountAsync();
        var items = await _db.Products.OrderBy(p => p.Id).Skip(page * size).Take(size).ToListAsync();

        return Ok(new { page, size, totalRecords = total, items });
    }


    //It need to evaluated
    [HttpPost("bulk")]
    public async Task<IActionResult> Bulk([FromBody] IEnumerable<CreateProductDto> dtos)
    {
        var entities = dtos.Select(dto => new Product
        {
            ProductHeading = dto.ProductHeading,
            Description = dto.Description,
            Price = dto.Price,
            Rating = dto.Rating,
            SubscriberCount = dto.SubscriberCount,
            ImageUrl = dto.ImageUrl
        }).ToList();

        _db.Products.AddRange(entities);
        await _db.SaveChangesAsync();
        return Ok(entities);
    }

}
*/

/*
[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repo;
    public ProductsController(IProductRepository repo) => _repo = repo;

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        var pro = new Product
        {
            ProductHeading = dto.ProductHeading,
            Description = dto.Description,
            Price = dto.Price,
            Rating = dto.Rating,
            SubscriberCount = dto.SubscriberCount,
            ImageUrl = dto.ImageUrl
        };

        await _repo.AddAsync(pro);
        return CreatedAtAction(nameof(GetById), new { id = pro.Id }, pro);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _repo.GetAllAsync());
    }


    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var p = await _repo.GetByIdAsync(id);
        if (p == null) return NotFound();
        return Ok(p);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateProductDto dto)
    {
        var pro = await _repo.GetByIdAsync(id);
        if (pro == null) return NotFound();

        pro.ProductHeading = dto.ProductHeading;
        pro.Description = dto.Description;
        pro.Price = dto.Price;
        pro.Rating = dto.Rating;
        pro.SubscriberCount = dto.SubscriberCount;
        pro.ImageUrl = dto.ImageUrl;

        await _repo.UpdateAsync(pro);
        return Ok(pro);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var pro = await _repo.GetByIdAsync(id);
        if (pro == null) return NotFound();

        await _repo.DeleteAsync(pro);
        return NoContent();
    }


    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? q)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return BadRequest("Provide search query");
        }

        var items = await _repo.SearchAsync(q);

        return Ok(items);
    }

    [HttpGet("page")]
    public async Task<IActionResult> Page([FromQuery] int page = 0, [FromQuery] int size = 10)
    {
        if (page < 0) page = 0;
        if (size < 10) size = 10;

        var all = (await _repo.GetAllAsync()).OrderBy(p => p.Id);
        var total = all.Count();
        var items = all.Skip(page * size).Take(size);

        return Ok(new { page, size, totalRecords = total, items });
    }


    //It needs to evaluated
    [HttpPost("bulk")]
    public async Task<IActionResult> Bulk([FromBody] IEnumerable<CreateProductDto> dtos)
    {
        var entities = dtos.Select(dto => new Product
        {
            ProductHeading = dto.ProductHeading,
            Description = dto.Description,
            Price = dto.Price,
            Rating = dto.Rating,
            SubscriberCount = dto.SubscriberCount,
            ImageUrl = dto.ImageUrl
        }).ToList();

        await _repo.BulkAddAsync(entities);
        return Ok(entities);
    }

}
*/


[ApiController]
[Route("api/v1/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _svc;
    public ProductsController(IProductService svc) => _svc = svc;

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        var created = await _svc.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

    [HttpGet("getById/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var p = await _svc.GetByIdAsync(id);
        return p == null ? NotFound() : Ok(p);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateProductDto dto)
    {
        var updated = await _svc.UpdateAsync(id, dto);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _svc.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? q)
    {
        if (string.IsNullOrWhiteSpace(q)) return BadRequest("Provide query 'q'.");
        var items = await _svc.SearchAsync(q);
        return Ok(items);
    }

    [HttpGet("page")]
    public async Task<IActionResult> Page([FromQuery] int page = 0, [FromQuery] int size = 10)
    {
        if (page < 0) page = 0;
        if (size <= 0) size = 10;
        var (items, total) = await _svc.GetPagedAsync(page, size);
        return Ok(new { page, size, totalRecords = total, items });
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> Bulk([FromBody] IEnumerable<CreateProductDto> dtos)
    {
        if (dtos == null) return BadRequest("Provide non-empty array of products.");
        var created = await _svc.BulkCreateAsync(dtos);
        return Ok(created);
    }

    [HttpGet("query")]
    public async Task<IActionResult> Query(
    [FromQuery] string? q,
    [FromQuery] decimal? minPrice,
    [FromQuery] decimal? maxPrice,
    [FromQuery] double? minRating,
    [FromQuery] string? sortBy,
    [FromQuery] bool desc = false,
    [FromQuery] int page = 0,
    [FromQuery] int size = 10)
    {
        var (items, total) = await _svc.GetFilteredAsync(q, minPrice, maxPrice, minRating, sortBy, desc, page, size);
        return Ok(new { page, size, totalRecords = total, items });
    }
}
