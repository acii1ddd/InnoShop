using FluentValidation;
using MediatR;
using Shared.CQRS;
using Shared.Exceptions;
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

internal sealed class DeactivateUserCommandHandler(IUserRepository userRepository) 
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
        
        return Unit.Value;
    }
}

