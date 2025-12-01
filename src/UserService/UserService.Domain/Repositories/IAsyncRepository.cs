using System.Linq.Expressions;

namespace UserService.Domain.Repositories;

public interface IAsyncRepository<T>
{
    public Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken ct);

    public Task<T?> GetByIdAsync(Guid id, CancellationToken ct);

    public Task<Guid> AddAsync(T entity, CancellationToken ct);

    public Task UpdateAsync(T entity, CancellationToken ct);

    public Task DeleteAsync(T entity, CancellationToken ct);
}