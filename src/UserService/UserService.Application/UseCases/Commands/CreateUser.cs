using Shared.CQRS;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Domain.Enums;
using UserService.Domain.Repositories;

namespace UserService.Application.UseCases.Commands;

public sealed record CreateUserCommand(string Name, string Email, string Password) 
    : ICommand<CreateUserResult>;

public sealed record CreateUserResult(Guid UserId);

internal sealed class CreateUserCommandHandler(
    IUserRepository userRepository, IPasswordHasher passwordHasher) 
    : ICommandHandler<CreateUserCommand, CreateUserResult>
{
    public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken ct)
    {
        var passwordHash = passwordHasher.Hash(command.Password);
        
        var user = UserEntity.Create(
            Guid.NewGuid(),
            command.Name,
            command.Email,
            UserRole.Default,
            passwordHash,
            isActive: true,
            isEmailConfirmed: false
        );
        
        var userId = await userRepository.AddAsync(user, ct);

        return new CreateUserResult(userId);
    }
}