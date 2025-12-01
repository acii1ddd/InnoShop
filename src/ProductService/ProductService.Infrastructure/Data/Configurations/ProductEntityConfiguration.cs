using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Domain.Entities;

namespace ProductService.Infrastructure.Data.Configurations;

public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    private const int MaxLength = 1024;
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("products");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(MaxLength);
        
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(MaxLength);

        builder.Property(x => x.Price)
            .IsRequired();

        builder.Property(x => x.IsAvailable).IsRequired();
        
        builder.Property(x => x.UserId).IsRequired();
        
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}