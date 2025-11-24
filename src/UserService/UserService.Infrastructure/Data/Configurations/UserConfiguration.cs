using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    private const int MaxLength = 100;

    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(MaxLength);
        
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(MaxLength);

        builder.Property(x => x.Role)
            .IsRequired();

        builder.Property(x => x.PasswordHash).IsRequired();
        
        builder.Property(x => x.PasswordHash).IsRequired();
        
        builder.Property(x => x.IsEmailConfirmed).IsRequired();
    }
}