using FluentValidation;
using MediatR;
using Shared.CQRS;
using Shared.Exceptions;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Domain.Interfaces.Auth;
using UserService.Domain.Repositories;

namespace UserService.Application.UseCases.Commands.Recovery;

public sealed record ForgotPasswordCommand(string Email) 
    : ICommand;

public class ForgotPasswordCommandValidator 
    : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email must be a valid email address")
            .MaximumLength(255).WithMessage("Email must not exceed 255 characters");
    }
}

internal sealed class ForgotPasswordCommandHandler(
    IUserRepository userRepository,
    ITokenGenerator tokenGenerator,
    IEmailService emailService, 
    IPasswordResetRepository passwordResetRepository,
    IPasswordHasher passwordHasher)
        : ICommandHandler<ForgotPasswordCommand>
{
    private const int ResetPasswordTokenExpiresInMinutes = 30;
    
    public async Task<Unit> Handle(ForgotPasswordCommand command, CancellationToken ct)
    {
        var user = await userRepository.GetByEmailAsync(command.Email, ct);

        if (user is null)
        {
            throw new NotFoundException("User", command.Email);
        }
        
        var token = tokenGenerator.GenerateSecureToken();
        
        var newPassword = Guid.NewGuid().ToString()[..8];
        var newPasswordHash = passwordHasher.Hash(newPassword);
        
        var reset = PasswordReset.Create(user.Id, token, 
            DateTime.UtcNow.AddMinutes(ResetPasswordTokenExpiresInMinutes),
            newPasswordHash);
        _ = await passwordResetRepository.AddAsync(reset, ct);

        var resetLink = $"http://localhost:7878/api/reset-password?token={Uri.EscapeDataString(token)}";
        
        await emailService.SendPasswordResetAsync(user.Email, resetLink, newPassword, ct);

        return Unit.Value;
    }
}