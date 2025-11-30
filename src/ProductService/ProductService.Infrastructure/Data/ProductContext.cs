using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Data.Configurations;

namespace ProductService.Infrastructure.Data;

public class ProductContext(DbContextOptions<ProductContext> options) 
    : DbContext(options)
{
    public DbSet<ProductEntity> Products { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductEntityConfiguration).Assembly);
    }
}