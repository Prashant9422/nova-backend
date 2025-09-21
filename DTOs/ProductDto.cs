namespace NovaApp.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string ProductHeading { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public double Rating { get; set; }
    public int SubscriberCount { get; set; }
    public string? ImageUrl { get; set; }
}