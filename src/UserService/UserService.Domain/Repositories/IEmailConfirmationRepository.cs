using UserService.Domain.Entities;

namespace UserService.Domain.Repositories;

public interface IEmailConfirmationRepository
{
    public Task<EmailConfirmation?> GetByTokenAsync(string token, CancellationToken ct);
    
    public Task<Guid> AddAsync(EmailConfirmation emailConfirmation, CancellationToken ct);
    
    public Task UpdateAsync(EmailConfirmation emailConfirmation, CancellationToken ct);
    
    public Task DeleteAsync(EmailConfirmation emailConfirmation, CancellationToken ct);
}