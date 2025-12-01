using FluentValidation;
using MediatR;
using Shared.CQRS;
using Shared.Exceptions;
using UserService.Domain.Repositories;

namespace UserService.Application.UseCases.Commands.Update;

public sealed record UpdateUserRoleCommand(Guid Id, string Role) 
    : ICommand;

public class UpdateUserRoleCommandValidator 
    : AbstractValidator<UpdateUserRoleCommand>
{
    public UpdateUserRoleCommandValidator()
    {
        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required")
            .MaximumLength(50).WithMessage("Role must not exceed 50 characters");
    }
}
internal sealed class UpdateUserRoleCommandHandler(IUserRepository userRepository)
    : ICommandHandler<UpdateUserRoleCommand>
{
    public async Task<Unit> Handle(UpdateUserRoleCommand command, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(command.Id, ct);

        if (user is null)
        {
            throw new NotFoundException("User", command.Id);
        }
        
        user.ChangeRole(command.Role);
        
        await userRepository.UpdateAsync(user, ct);
        
        return Unit.Value;
    }
}