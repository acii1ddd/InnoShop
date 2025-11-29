namespace UserService.Domain.Interfaces.Auth;

public interface IEmailService
{
    public Task SendEmailConfirmationAsync(string email, string confirmationToken, CancellationToken ct);
    
    public Task SendPasswordResetAsync(Guid userId, string email, string resetToken, CancellationToken ct);
    
    public Task SendDeactivationNoticeAsync(Guid userId, string email, CancellationToken ct);
    
    Task SendActivationNoticeAsync(Guid userId, string email, CancellationToken ct);
}