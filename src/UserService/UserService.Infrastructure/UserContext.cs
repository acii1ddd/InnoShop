using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Infrastructure;

public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
}