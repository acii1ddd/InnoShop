using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories;

public class UserRepository(UserContext context) : IUserRepository
{
    public async Task<IReadOnlyList<UserEntity>> GetAllAsync(
        Expression<Func<UserEntity, bool>> predicate, CancellationToken ct)
    {
        return await context.Users
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync(cancellationToken: ct);
    }

    public async Task<UserEntity?> GetById(Guid id, CancellationToken ct)
    {
        return await context.Users
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: ct);
    }
    
    public async Task<Guid> AddAsync(UserEntity entity, CancellationToken ct)
    {
        await context.Users.AddAsync(entity, ct);

        await context.SaveChangesAsync(ct);
        
        return entity.Id;
    }
    
    public async Task UpdateAsync(UserEntity entity, CancellationToken ct)
    {
        context.Users.Update(entity);

        await context.SaveChangesAsync(ct);
    }
    
    public async Task DeleteAsync(UserEntity entity, CancellationToken ct)
    {
        context.Users.Remove(entity);

        await context.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<UserEntity>> GetPagedAsync(int pageIndex, int pageSize, CancellationToken ct)
    {
        return await context.Users
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }

    public async Task<int> GetCountAsync(CancellationToken ct)
    {
        return await context.Users.CountAsync(ct);
    }
}