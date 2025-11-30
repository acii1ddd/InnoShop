using Microsoft.AspNetCore.Http;
using ProductService.Domain.Interfaces;

namespace ProductService.Infrastructure.Tools;

public class UserContext(IHttpContextAccessor accessor) 
    : IUserContext
{
    public Guid GetUserId()
    {
        var userIdClaim = accessor.HttpContext?.User.FindFirst("userId")?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new UnauthorizedAccessException("UserId claim is missing.");
        }

        return Guid.Parse(userIdClaim);
    }
}