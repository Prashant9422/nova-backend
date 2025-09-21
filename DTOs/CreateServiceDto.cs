namespace NovaApp.DTOs;

public class CreateServiceDto
{
    public int Id { get; set; }
    public string ServiceHeading { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Pricing { get; set; }
    public int DeliveryDays { get; set; }
    public string? ImageUrl { get; set; }
}