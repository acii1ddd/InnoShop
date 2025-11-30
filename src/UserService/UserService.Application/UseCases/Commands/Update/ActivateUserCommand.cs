using FluentValidation;
using MediatR;
using Shared.CQRS;
using Shared.Exceptions;
using UserService.Domain.Repositories;

namespace UserService.Application.UseCases.Commands.Update;

public sealed record ActivateUserCommand(Guid Id) 
    : ICommand;

public class ActivateUserCommandValidator : AbstractValidator<ActivateUserCommand>
{
    public ActivateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}

internal sealed class ActivateUserCommandHandler(IUserRepository userRepository)
    : ICommandHandler<ActivateUserCommand>
{
    public async Task<Unit> Handle(ActivateUserCommand command, 
        CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(command.Id, ct);

        if (user is null)
        {
            throw new NotFoundException("User", command.Id);
        }
        
        user.Activate();
        
        await userRepository.UpdateAsync(user, ct);
        return Unit.Value;
    }
}