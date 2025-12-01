using UserService.Domain.Enums;

namespace UserService.Domain.Interfaces.Auth;

public interface ITokenGenerator
{
    public string GenerateAccessToken(Guid userId, UserRole role);

    public string GenerateSecureToken();
}