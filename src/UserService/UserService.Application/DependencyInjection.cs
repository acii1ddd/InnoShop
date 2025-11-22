using Microsoft.Extensions.DependencyInjection;
using UserService.Application.UseCases.Commands;

namespace UserService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<CreateHandler>();
        
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(CreateHandler).Assembly);

            // todo behaviors
            //config.AddBehavior < typeof(LoggingBehavior) > ();
            //config.AddBehavior < typeof(ValidationBehavior) > ();
        });
        
        return services;
    }
}