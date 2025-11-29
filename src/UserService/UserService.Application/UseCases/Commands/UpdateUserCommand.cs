using FluentValidation;
using MediatR;
using Shared.CQRS;
using Shared.Exceptions;
using UserService.Domain.Enums;
using UserService.Domain.Repositories;

namespace UserService.Application.UseCases.Commands;

public sealed record UpdateUserCommand(Guid Id, string Name, string Email, string Role) 
    : ICommand;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("User Id is required for update");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(5).WithMessage("Name must be at least 5 characters long")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email must be a valid email address");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required")
            .MaximumLength(50).WithMessage("Role must not exceed 50 characters");
    }
}

internal sealed class UpdateUserCommandHandler(IUserRepository userRepository)
    : ICommandHandler<UpdateUserCommand>
{
    public async Task<Unit> Handle(UpdateUserCommand command, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(command.Id, ct);

        if (user is null)
        {
            throw new NotFoundException("User", command.Id);
        }
        
        user.ChangeName(command.Name);
        user.ChangeEmail(command.Email);
        user.ChangeRole(command.Role);
        
        await userRepository.UpdateAsync(user, ct);
        
        return Unit.Value;
    }
}