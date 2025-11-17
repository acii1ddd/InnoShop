using System.Linq.Expressions;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;

namespace UserService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public IEnumerable<UserEntity> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<UserEntity>> GetAllAsync(
        Expression<Func<UserEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<UserEntity?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> AddAsync(UserEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UserEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(UserEntity entity)
    {
        throw new NotImplementedException();
    }

    Task<IReadOnlyList<UserEntity>> IAsyncRepository<UserEntity>.GetAllAsync()
    {
        throw new NotImplementedException();
    }
}