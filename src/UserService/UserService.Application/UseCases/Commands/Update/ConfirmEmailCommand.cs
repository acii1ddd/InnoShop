using Shared.CQRS;
using Shared.Exceptions;
using UserService.Domain.Repositories;

namespace UserService.Application.UseCases.Commands.Update;

public sealed record ConfirmEmailCommand(string Token) 
    : ICommand<ConfirmEmailResult>;

public sealed record ConfirmEmailResult(string Message);

internal sealed class ConfirmEmailCommandHandler(
    IUserRepository userRepository, 
    IEmailConfirmationRepository emailConfirmationRepository) 
        : ICommandHandler<ConfirmEmailCommand, ConfirmEmailResult>
{
    public async Task<ConfirmEmailResult> Handle(
        ConfirmEmailCommand command, 
        CancellationToken ct)
    {
        var emailConfirmation = await emailConfirmationRepository.GetByTokenAsync(command.Token, ct);

        if (emailConfirmation is null)
        {
            throw new NotFoundException("EmailConfirmation", command.Token);
        }
        
        if (emailConfirmation.User.IsEmailConfirmed)
        {
            throw new BadRequestException("Email is already confirmed");
        }

        emailConfirmation.ValidateToken(command.Token);
        emailConfirmation.User.ConfirmEmailAsync();
        await userRepository.UpdateAsync(emailConfirmation.User, ct);
        
        await emailConfirmationRepository.DeleteAsync(emailConfirmation, ct);
        
        return new ConfirmEmailResult("Email successfully confirmed");
    }
}