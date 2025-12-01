using Shared.CQRS;
using Shared.Exceptions;
using UserService.Domain.Entities;
using UserService.Domain.Enums;
using UserService.Domain.Interfaces;
using UserService.Domain.Interfaces.Auth;
using UserService.Domain.Repositories;

namespace UserService.Application.UseCases.Commands.Auth;

public record RegisterUserCommand(string Name, string Email, string Password) 
    : ICommand<RegisterUserResult>;

public record RegisterUserResult(Guid UserId);

public class RegisterUserCommandHandler(
    IUserRepository userRepository, 
    IPasswordHasher passwordHasher,
    ITokenGenerator tokenGenerator, 
    IEmailConfirmationRepository emailConfirmationRepository,
    IEmailService emailService) 
        : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    private const int ConfirmationTokenExpiresInMinutes = 30;
    
    public async Task<RegisterUserResult> Handle(
        RegisterUserCommand command, 
        CancellationToken ct)
    {
        var user = await userRepository.GetByEmailAsync(command.Email, ct);
        if (user is not null)
        {
            throw new BadRequestException("User with this email already exists");
        }
        
        var passwordHash = passwordHasher.Hash(command.Password);
        
        var newUser = UserEntity.Create(
            Guid.NewGuid(),
            command.Name,
            command.Email,
            UserRole.Default,
            passwordHash,
            isActive: true,
            isEmailConfirmed: false
        );
        
        var userId = await userRepository.AddAsync(newUser, ct);
        
        var token = tokenGenerator.GenerateSecureToken();
        
        var emailConfirmation = EmailConfirmation.Create(userId, token, 
            DateTime.UtcNow.AddMinutes(ConfirmationTokenExpiresInMinutes));
        
        _ = await emailConfirmationRepository.AddAsync(emailConfirmation, ct);

        var confirmationLink = $"http://localhost:7878/api/confirm-email?token={Uri.EscapeDataString(token)}";
        
        await emailService.SendEmailConfirmationAsync(
            newUser.Email, confirmationLink, ct
        );
        
        return new RegisterUserResult(newUser.Id);
    }
}