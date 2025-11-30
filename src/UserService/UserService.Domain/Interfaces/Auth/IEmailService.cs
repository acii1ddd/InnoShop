namespace UserService.Domain.Interfaces.Auth;

public interface IEmailService
{
    public Task SendEmailConfirmationAsync(string email, string confirmationToken, CancellationToken ct);

    public Task SendPasswordResetAsync(string email, string resetLink, string newPassword, CancellationToken ct);
    
    public Task SendDeactivationNoticeAsync(Guid userId, string email, CancellationToken ct);
    
    Task SendActivationNoticeAsync(Guid userId, string email, CancellationToken ct);
}