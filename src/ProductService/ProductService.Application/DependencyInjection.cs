using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.UseCases.Commands;
using Shared.Behavior;
using Shared.Exceptions.Handler;

namespace ProductService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(CreteProductCommandHandler).Assembly);

            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddExceptionHandler<ExceptionHandler>();
        
        services.AddValidatorsFromAssembly(typeof(CreteProductValidator).Assembly);
        
        return services;
    }
}