using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Domain.Repositories;
using UserService.Infrastructure.Data;
using UserService.Infrastructure.Repositories;

namespace UserService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Connection string not found");
            
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}