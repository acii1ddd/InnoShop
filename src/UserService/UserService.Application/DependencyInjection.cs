using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Behavior;
using Shared.Exceptions.Handler;
using UserService.Application.UseCases.Commands;

namespace UserService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);

            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddExceptionHandler<ExceptionHandler>();
        
        services.AddValidatorsFromAssembly(typeof(ActivateUserCommandValidator).Assembly);
        
        return services;
    }
}