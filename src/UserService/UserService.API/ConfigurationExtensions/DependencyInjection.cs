using UserService.API.EndpointsSettings;


namespace UserService.API.ConfigurationExtensions;

public static class DependencyInjection
{
        extension(IServiceCollection services)
        {
            public IServiceCollection AddApiServices(IConfiguration configuration)
            {
                services
                    .AddOpenApiSpec()
                    .AddEndpoints(typeof(Program).Assembly);
        
                return services;
            }

            private IServiceCollection AddOpenApiSpec()
            {
                services.AddOpenApi();
        
                return services;
            }
        }
}