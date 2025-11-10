using UserService.Domain.Entities;

namespace UserService.Domain.Repositories;

public interface IUserRepository : IAsyncRepository<UserEntity>
{
}