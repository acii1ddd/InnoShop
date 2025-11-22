using UserService.Domain.Entities;

namespace UserService.Domain.Repositories;

public interface IUserRepository : IAsyncRepository<UserEntity>
{
    public Task<IReadOnlyList<UserEntity>> GetPagedAsync(int pageIndex, int pageSize, CancellationToken ct);
    
    public Task<int> GetCountAsync(CancellationToken ct);
}