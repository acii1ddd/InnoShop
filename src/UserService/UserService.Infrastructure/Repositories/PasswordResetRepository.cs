using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories;

public class PasswordResetRepository(UserContext context) 
    : IPasswordResetRepository
{
    public async Task<PasswordReset?> GetByTokenAsync(string token, CancellationToken ct)
    {
        return await context.PasswordResets
            .AsNoTracking()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == token, ct);
    }

    public async Task<Guid> AddAsync(PasswordReset passwordReset, CancellationToken ct)
    {
        await context.PasswordResets.AddAsync(passwordReset, ct);
        
        await context.SaveChangesAsync(ct);

        return passwordReset.Id;
    }

    public async Task UpdateAsync(PasswordReset passwordReset, CancellationToken ct)
    {
        context.PasswordResets.Update(passwordReset);
        
        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(PasswordReset passwordReset, CancellationToken ct)
    {
        context.PasswordResets.Remove(passwordReset);
        
        await context.SaveChangesAsync(ct);
    }
}