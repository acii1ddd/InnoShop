using MediatR;
using Shared.CQRS;
using Shared.Exceptions;
using UserService.Domain.Enums;
using UserService.Domain.Repositories;

namespace UserService.Application.UseCases.Commands;

public sealed record UpdateUserCommand(Guid Id, string Name, string Email, string Role) 
    : ICommand;

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
        user.ChangeRole(Enum.Parse<UserRole>(command.Role));
        
        await userRepository.UpdateAsync(user, ct);
        
        return Unit.Value;
    }
}