using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Infrastructure.Data.Configurations;

namespace UserService.Infrastructure.Data;

public class UserContext(DbContextOptions<UserContext> options)
    : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    
    public DbSet<EmailConfirmation> EmailConfirmations { get; set; }

    public DbSet<PasswordReset> PasswordResets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}