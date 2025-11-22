using UserService.Infrastructure;

namespace User.API.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi();

        services.AddInfrastructure(configuration);
        
        return services;
    }
}