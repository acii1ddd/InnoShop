using UserService.Domain.Entities;
using UserService.Domain.Interfaces.Repositories;

namespace UserService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public IEnumerable<UserEntity> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}