using FluentValidation;
using MediatR;
using Shared.CQRS;
using Shared.Exceptions;
using UserService.Application.Interfaces;
using UserService.Domain.Repositories;

namespace UserService.Application.UseCases.Commands.Update;

public sealed record DeactivateUserCommand(Guid Id) 
    : ICommand;

public class DeactivateUserCommandValidator : AbstractValidator<DeactivateUserCommand>
{
    public DeactivateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("User Id is required for deactivation");
    }
}

internal sealed class DeactivateUserCommandHandler(
    IUserRepository userRepository,
    IProductServiceClient productServiceClient) 
    : ICommandHandler<DeactivateUserCommand>
{
    public async Task<Unit> Handle(DeactivateUserCommand command, 
        CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(command.Id, ct);

        if (user is null)
        {
            throw new NotFoundException("User", command.Id);
        }
        
        user.Deactivate();
        
        await userRepository.UpdateAsync(user, ct);
        
        // Get all user products
        var products = await productServiceClient
            .GetAvailableProductsByUserIdAsync(command.Id, ct);

        // Mark each product as available
        foreach (var product in products)
        {
            _ = await productServiceClient.MarkProductAsUnavailableAsync(product.ProductInfo.Id, ct);
        }
        
        return Unit.Value;
    }
}

