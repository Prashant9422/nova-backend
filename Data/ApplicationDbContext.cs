using Microsoft.EntityFrameworkCore;
using NovaApp.Models;
namespace NovaApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, ProductHeading = "Sample Product A", Description = "A sample product", Price = 49.99m, Rating = 4.5, SubscriberCount = 120, ImageUrl = null },
            new Product { Id = 2, ProductHeading = "Sample Product B", Description = "Another product", Price = 29.99m, Rating = 4.0, SubscriberCount = 80, ImageUrl = null }
        );

        modelBuilder.Entity<Service>().HasData(
            new Service { Id = 1, ServiceHeading = "Web Development", Description = "Fullstack web dev", Pricing = 1500m, DeliveryDays = 30, ImageUrl = null },
            new Service { Id = 2, ServiceHeading = "Mobile App", Description = "iOS & Android app", Pricing = 2500m, DeliveryDays = 45, ImageUrl = null }
        );

        modelBuilder.Entity<Employee>().HasData(
            new Employee { Id = 1, FullName = "Alice Johnson", Email = "alice@example.com", Position = "CTO", Department = "Engineering", CoreTeam = true, ProfileImageUrl = null },
            new Employee { Id = 2, FullName = "Bob Singh", Email = "bob@example.com", Position = "Product Manager", Department = "Product", CoreTeam = true, ProfileImageUrl = null }
        );
    }
}