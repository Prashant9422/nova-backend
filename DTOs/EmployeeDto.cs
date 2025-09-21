namespace NovaApp.DTOs;

public class EmployeeDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Position { get; set; } = null!;
    public string? Department { get; set; }
    public bool CoreTeam { get; set; }
    public string? ProfileImageUrl { get; set; }
}