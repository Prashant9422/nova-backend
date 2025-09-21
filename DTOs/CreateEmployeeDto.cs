namespace NovaApp.DTOs;

public class CreateEmployeeDto
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Position { get; set; } = null!;
    public string? Department { get; set; }
    public bool CoreTeam { get; set; }
    public string? ProfileImageUrl { get; set; }
}

