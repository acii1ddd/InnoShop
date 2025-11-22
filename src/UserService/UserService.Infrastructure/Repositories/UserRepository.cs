using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories;

public class UserRepository(UserContext context) : IUserRepository
{
    public async Task<IReadOnlyList<UserEntity>> GetAllAsync()
    {
        return await context.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IReadOnlyList<UserEntity>> GetAllAsync(
        Expression<Func<UserEntity, bool>> predicate)
    {
        return await context.Users
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
    }

    public async Task<UserEntity?> GetById(Guid id)
    {
        return await context.Users
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<Guid> AddAsync(UserEntity entity)
    {
        await context.Users.AddAsync(entity);

        await context.SaveChangesAsync();
        
        return entity.Id;
    }
    public async Task UpdateAsync(UserEntity entity)
    {
        context.Users.Update(entity);

        await context.SaveChangesAsync();
    }
    public async Task DeleteAsync(UserEntity entity)
    {
        context.Users.Remove(entity);

        await context.SaveChangesAsync();
    }
}