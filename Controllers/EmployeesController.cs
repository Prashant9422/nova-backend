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
public class EmployeesController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public EmployeesController(ApplicationDbContext db) => _db = db;

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
    {
        var emp = new Employee
        {
            FullName = dto.FullName,
            Position = dto.Position,
            Email = dto.Email,
            Department = dto.Department,
            CoreTeam = dto.CoreTeam,
            ProfileImageUrl = dto.ProfileImageUrl
        };

        await _db.Employees.AddAsync(emp);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = emp.Id }, emp);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _db.Employees.ToListAsync());
    }

    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var emp = await _db.Employees.FindAsync(id);
        if (emp == null) return NotFound();

        return Ok(emp);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateEmployeeDto dto)
    {
        var emp = await _db.Employees.FindAsync(id);
        if (emp == null) return NotFound();

        emp.FullName = dto.FullName;
        emp.Position = dto.Position;
        emp.Email = dto.Email;
        emp.Department = dto.Department;
        emp.CoreTeam = dto.CoreTeam;
        emp.ProfileImageUrl = dto.ProfileImageUrl;

        await _db.SaveChangesAsync();
        return Ok(emp);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var emp = await _db.Employees.FindAsync(id);
        if (emp == null) return NotFound();

        _db.Employees.Remove(emp);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("core-team")]
    public async Task<IActionResult> GetCoreTeam()
    {
        var items = await _db.Employees.Where(x => x.CoreTeam).ToListAsync();
        return Ok(items);
    }


    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? q)
    {
        // var query = _db.Employees.AsQueryable();
        if (string.IsNullOrWhiteSpace(q))
        {
            return BadRequest("Provide query 'q'.");
        }

        var items = await _db.Employees
            .Where(x => (x.FullName ?? "").Contains(q) || (x.Email ?? "").Contains(q) || (x.Position ?? "").Contains(q))
            .ToListAsync();

        return Ok(items);

    }
}
*/


/*
[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _repo;
    public EmployeesController(IEmployeeRepository repo) => _repo = repo;

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
    {
        var emp = new Employee
        {
            FullName = dto.FullName,
            Position = dto.Position,
            Email = dto.Email,
            Department = dto.Department,
            CoreTeam = dto.CoreTeam,
            ProfileImageUrl = dto.ProfileImageUrl
        };

        await _repo.AddAsync(emp);
        return CreatedAtAction(nameof(GetById), new { id = emp.Id }, emp);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _repo.GetAllAsync());
    }

    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var emp = await _repo.GetByIdAsync(id);
        if (emp == null) return NotFound();

        return Ok(emp);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateEmployeeDto dto)
    {
        var emp = await _repo.GetByIdAsync(id);
        if (emp == null) return NotFound();

        emp.FullName = dto.FullName;
        emp.Position = dto.Position;
        emp.Email = dto.Email;
        emp.Department = dto.Department;
        emp.CoreTeam = dto.CoreTeam;
        emp.ProfileImageUrl = dto.ProfileImageUrl;

        await _repo.UpdateAsync(emp);
        return Ok(emp);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var emp = await _repo.GetByIdAsync(id);
        if (emp == null) return NotFound();

        await _repo.DeleteAsync(emp);
        return NoContent();
    }

    [HttpGet("core-team")]
    public async Task<IActionResult> GetCoreTeam()
    {
        var items = await _repo.GetCoreTeamAsync();
        return Ok(items);
    }

    //Wrong Implementation
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
[Route("api/v1/employees")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _svc;
    public EmployeesController(IEmployeeService svc) => _svc = svc;

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
    {
        var created = await _svc.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

    [HttpGet("getById/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var e = await _svc.GetByIdAsync(id);
        return e == null ? NotFound() : Ok(e);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateEmployeeDto dto)
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

    [HttpGet("core-team")]
    public async Task<IActionResult> GetCoreTeam() => Ok(await _svc.GetCoreTeamAsync());

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? q)
    {
        if (string.IsNullOrWhiteSpace(q)) return BadRequest("Provide query 'q'.");
        var items = await _svc.SearchAsync(q);
        return Ok(items);
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> Bulk([FromBody] IEnumerable<CreateEmployeeDto> dtos)
    {
        if (dtos == null) return BadRequest("Provide non-empty array of products.");
        var created = await _svc.BulkCreateAsync(dtos);
        return Ok(created);
    }
}




