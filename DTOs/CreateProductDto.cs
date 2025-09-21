namespace NovaApp.DTOs;

public class CreateProductDto
{
    public string ProductHeading { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public double Rating { get; set; }
    public int SubscriberCount { get; set; }
    public string? ImageUrl { get; set; }
}