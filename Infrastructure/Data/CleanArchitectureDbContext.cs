using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class CleanArchitectureDbContext : DbContext
    {
        public CleanArchitectureDbContext(DbContextOptions<CleanArchitectureDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Product>()
                .HasData(
                    new Product
                    {
                        Id = 1,
                        Label = "Samsung Tv",
                        Description = "The last, powerfull TV of Samsung",
                        Price = 1599.99m
                    });
        }
    }
}
