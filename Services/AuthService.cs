using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NovaApp.DTOs;
using NovaApp.Models;
using NovaApp.Repositories;

namespace NovaApp.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly IConfiguration _config;
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly IMapper _mapper;
    public AuthService(IUserRepository userRepo, IConfiguration config, IMapper mapper)
    {
        _userRepo = userRepo;
        _config = config;
        _passwordHasher = new PasswordHasher<User>();
        _mapper = mapper;
    }
    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepo.GetByUsernameAsync(loginDto.Username);
        if (user == null)
        {
            throw new ApplicationException("Invalid username or password.");
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            throw new ApplicationException("Invalid username or password.");
        }

        return GenerateToken(user);
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        var existing = await _userRepo.GetByUsernameAsync(registerDto.Username);
        if (existing != null)
        {
            throw new ApplicationException("Username is already exists");
        }

        // Map DTO -> entity (PasswordHash must be set explicitly)
        var user = _mapper.Map<User>(registerDto);

        // Ensure defaults when mapping didn't set them
        if (string.IsNullOrWhiteSpace(user.Role))
            user.Role = "User";

        // Hash and set password
        user.PasswordHash = _passwordHasher.HashPassword(user, registerDto.Password);

        // CreatedAt default is set by model (or set here)
        user.CreatedAt = DateTime.UtcNow;

        await _userRepo.AddUserAsync(user);

        // Generate token and response (token contains role/claims)
        return GenerateToken(user);

        // var user = new User
        // {
        //     Username = registerDto.Username,
        //     Email = registerDto.Email,
        //     Role = string.IsNullOrWhiteSpace(registerDto.Role) ? "User" : registerDto.Role
        // };

        // user.PasswordHash = _passwordHasher.HashPassword(user, registerDto.Password);

        // await _userRepo.AddUserAsync(user);
        // return GenerateToken(user);
    }

    private AuthResponseDto GenerateToken(User user)
    {
        var jwtSection = _config.GetSection("Jwt");
        var key = jwtSection.GetValue<string>("Key");
        var issuer = jwtSection.GetValue<string>("Issuer");
        var audience = jwtSection.GetValue<string>("Audience");
        var expiryMinutes = jwtSection.GetValue<int>("ExpiryMinutes");

        if (string.IsNullOrEmpty(key))
        {
            throw new ApplicationException("JWT key is not configured properly.");
        }
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return new AuthResponseDto
        {
            Token = tokenString,
            ExpiresAt = token.ValidTo,
            Username = user.Username,
            Role = user.Role
        };


    }
}