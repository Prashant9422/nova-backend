
namespace NovaApp.Models;

public class Service
{
    public int Id { get; set; }
    public string ServiceHeading { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Pricing { get; set; }
    public int DeliveryDays { get; set; }
    public string? ImageUrl { get; set; }

    public static implicit operator int(Service? v)
    {
        throw new NotImplementedException();
    }
}