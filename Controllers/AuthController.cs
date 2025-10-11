using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using NovaApp.DTOs;
using NovaApp.Services;

namespace NovaApp.Controllers;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    public AuthController(IAuthService auth) => _auth = auth;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        try
        {
            var res = await _auth.RegisterAsync(dto);
            return Ok(res);
        }
        catch
        {
            return BadRequest("Registration failed");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var res = await _auth.LoginAsync(dto);
        if (res == null) return Unauthorized("Invalid credentials");
        return Ok(res);
    }

}