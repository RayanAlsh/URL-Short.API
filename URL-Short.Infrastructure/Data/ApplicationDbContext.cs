using Microsoft.EntityFrameworkCore;
using URL_Short.Core;

namespace URL_Short.Infrastructure;

public class ApplicationDbContext : DbContext
{
    private readonly IPasswordHasher _passwordHasher;

    public ApplicationDbContext(DbContextOptions options, IPasswordHasher passwordHasher) : base(options)
    {
        _passwordHasher = passwordHasher;
    }
    public DbSet<URL> Transactions { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed admin user
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.NewGuid(),
                Email = "admin@example.com", // Replace with your admin email
                Password = _passwordHasher.HashPassword("qwerty1234567"), // Use the utility method
                Role = "Admin"
            }
        );

        base.OnModelCreating(modelBuilder);
    }

}