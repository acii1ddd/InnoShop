namespace User.API;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddOpenApi();
        
        return services;
    }
}