using UserService.Domain.Entities;

namespace UserService.Domain.Repositories;

public interface IPasswordResetRepository
{
    public Task<PasswordReset?> GetByTokenAsync(string token, CancellationToken ct);
    
    public Task<Guid> AddAsync(PasswordReset passwordReset, CancellationToken ct);
    
    public Task UpdateAsync(PasswordReset passwordReset, CancellationToken ct);
    
    public Task DeleteAsync(PasswordReset passwordReset, CancellationToken ct);
}