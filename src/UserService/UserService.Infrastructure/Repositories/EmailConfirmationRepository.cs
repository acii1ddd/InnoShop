using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories;

public class EmailConfirmationRepository(UserContext context) 
    : IEmailConfirmationRepository
{
    public async Task<EmailConfirmation?> GetByTokenAsync(string token, CancellationToken ct)
    {
        return await context.EmailConfirmations
            .AsNoTracking()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == token, ct);
    }
    
    public async Task<Guid> AddAsync(EmailConfirmation emailConfirmation, CancellationToken ct)
    {
        await context.EmailConfirmations.AddAsync(emailConfirmation, ct);
        
        await context.SaveChangesAsync(ct);

        return emailConfirmation.Id;
    }

    public async Task UpdateAsync(EmailConfirmation emailConfirmation, CancellationToken ct)
    {
        context.EmailConfirmations.Update(emailConfirmation);
        
        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(EmailConfirmation emailConfirmation, CancellationToken ct)
    {
        context.EmailConfirmations.Remove(emailConfirmation);
        
        await context.SaveChangesAsync(ct);
    }
}