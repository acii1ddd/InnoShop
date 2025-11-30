using MediatR;
using Shared.CQRS;
using Shared.Exceptions;
using UserService.Domain.Interfaces;
using UserService.Domain.Repositories;

namespace UserService.Application.UseCases.Commands.Recovery;

public sealed record ResetPasswordCommand(string Token) 
    : ICommand<ResetPasswordResult>;

public sealed record ResetPasswordResult(string Message);

internal sealed class ResetPasswordCommandHandler(
    IPasswordResetRepository passwordResetRepository,
    IUserRepository userRepository
    ) 
        : ICommandHandler<ResetPasswordCommand, ResetPasswordResult>
{
    public async Task<ResetPasswordResult> Handle(ResetPasswordCommand command, CancellationToken ct)
    {
        var passwordReset = await passwordResetRepository.GetByTokenAsync(command.Token, ct);

        if (passwordReset is null)
        {
            throw new NotFoundException("PasswordReset", command.Token);
        }

        passwordReset.ValidateToken();
        passwordReset.User.ChangePassword(passwordReset.GeneratedPasswordHash);
        await userRepository.UpdateAsync(passwordReset.User, ct);
        
        await passwordResetRepository.DeleteAsync(passwordReset, ct);

        return new ResetPasswordResult("Password has been successfully reset");
    }
}