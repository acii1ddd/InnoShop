using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Data.Configurations;

public class EmailConfirmationConfiguration : IEntityTypeConfiguration<EmailConfirmation>
{
    private const int MaxLength = 256;
    public void Configure(EntityTypeBuilder<EmailConfirmation> builder)
    {
        builder.ToTable("emailConfirmations");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.UserId).IsRequired();
        
        builder.Property(x => x.Token)
            .IsRequired()
            .HasMaxLength(MaxLength);

        builder.Property(x => x.TokenExpiresAt).IsRequired();
        
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.EmailConfirmations)
            .HasForeignKey(x => x.UserId);
    }
}