using UserService.Infrastructure;

namespace UserService.API.ConfigurationExtensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi();

        services.AddInfrastructureServices(configuration);
        
        return services;
    }
}