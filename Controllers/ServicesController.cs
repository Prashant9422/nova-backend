using System.ComponentModel.Design;
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
public class ServicesController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public ServicesController(ApplicationDbContext db) => _db = db;

    [HttpPost("cretae")]
    public async Task<IActionResult> Create([FromBody] CreateServiceDto dto)
    {
        var serv = new Service
        {
            ServiceHeading = dto.ServiceHeading,
            Description = dto.Description,
            Pricing = dto.Pricing,
            DeliveryDays = dto.DeliveryDays,
            ImageUrl = dto.ImageUrl
        };

        await _db.Services.AddAsync(serv);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = serv.Id }, serv);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _db.Services.ToListAsync());
    }

    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var p = await _db.Services.FindAsync(id);
        if (p == null) return NotFound();

        return Ok(p);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateServiceDto dto)
    {
        var serv = await _db.Services.FindAsync(id);
        if (serv == null) return NotFound();

        serv.ServiceHeading = dto.ServiceHeading;
        serv.Description = dto.Description;
        serv.Pricing = dto.Pricing;
        serv.DeliveryDays = dto.DeliveryDays;
        serv.ImageUrl = dto.ImageUrl;

        await _db.SaveChangesAsync();
        return Ok(serv);
    }

    [HttpDelete("detelte/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serv = await _db.Services.FindAsync(id);
        if (serv == null) return NotFound();

        _db.Services.Remove(serv);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? q)
    {
        var ser = _db.Services.AsQueryable();
        if (string.IsNullOrWhiteSpace(q))
        {
            return BadRequest("Provide Query 'q'.");
        }

        var items = await _db.Services
            .Where(x => (x.ServiceHeading ?? "").Contains(q) || (x.Description ?? "").Contains(q))
            .ToListAsync();

        return Ok(items);
    }

} 

*/


/*
[ApiController]
[Route("api/v1/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IServiceRepository _repo;
    public ServicesController(IServiceRepository repo) => _repo = repo;

    [HttpPost("cretae")]
    public async Task<IActionResult> Create([FromBody] CreateServiceDto dto)
    {
        var serv = new Service
        {
            ServiceHeading = dto.ServiceHeading,
            Description = dto.Description,
            Pricing = dto.Pricing,
            DeliveryDays = dto.DeliveryDays,
            ImageUrl = dto.ImageUrl
        };

        await _repo.AddAsync(serv);
        return CreatedAtAction(nameof(GetById), new { id = serv.Id }, serv);
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
    public async Task<IActionResult> Update(int id, [FromBody] CreateServiceDto dto)
    {
        var serv = await _repo.GetByIdAsync(id);
        if (serv == null) return NotFound();

        serv.ServiceHeading = dto.ServiceHeading;
        serv.Description = dto.Description;
        serv.Pricing = dto.Pricing;
        serv.DeliveryDays = dto.DeliveryDays;
        serv.ImageUrl = dto.ImageUrl;

        await _repo.UpdateAsync(serv);
        return Ok(serv);
    }

    [HttpDelete("detelte/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serv = await _repo.GetByIdAsync(id);
        if (serv == null) return NotFound();

        await _repo.DeleteAsync(serv);
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

}
*/




[ApiController]
[Route("api/v1/services")]
public class ServicesController : ControllerBase
{
    private readonly IServiceService _svc;
    public ServicesController(IServiceService svc) => _svc = svc;

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateServiceDto dto)
    {
        var created = await _svc.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var s = await _svc.GetByIdAsync(id);
        return s == null ? NotFound() : Ok(s);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateServiceDto dto)
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
}
