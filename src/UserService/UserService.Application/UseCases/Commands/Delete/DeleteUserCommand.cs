using FluentValidation;
using MediatR;
using Shared.CQRS;
using Shared.Exceptions;
using UserService.Domain.Repositories;

namespace UserService.Application.UseCases.Commands.Delete;

public sealed record DeleteUserCommand(Guid Id) 
    : ICommand;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("User Id is required for deletion");
    }
}

internal sealed class DeleteUserCommandHandler(IUserRepository userRepository)
    : ICommandHandler<DeleteUserCommand>
{
    public async Task<Unit> Handle(DeleteUserCommand command, 
        CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(command.Id, ct);

        if (user is null)
        {
            throw new NotFoundException("User", command.Id);
        }

        await userRepository.DeleteAsync(user, ct);
        
        return Unit.Value;
    }
}