using System.Linq.Expressions;

namespace UserService.Domain.Repositories;

public interface IAsyncRepository<T>
{
    public Task<IReadOnlyList<T>> GetAllAsync();

    public Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

    public Task<T?> GetById(Guid id);

    public Task<Guid> AddAsync(T entity);

    public Task UpdateAsync(T entity);

    public Task DeleteAsync(T entity);
}