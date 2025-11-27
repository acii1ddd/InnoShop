using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserService.Domain.Enums;
using UserService.Domain.Interfaces.Auth;

namespace UserService.Infrastructure.Services;

public class JwtAccessTokenGenerator(IConfiguration config) : ITokenGenerator
{
    public string GenerateAccessToken(Guid userId, UserRole role)
    {
        var issuer = config["AuthSettings:Issuer"];
        var audience = config["AuthSettings:Audience"];
        var lifetime = int.Parse(config["AuthSettings:Lifetime"] ?? "60");
        var secret = config["AuthSettings:Secret"];
        
        // дата окончания срока жизни токена
        var expires = DateTime.UtcNow.AddMinutes(lifetime);

        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
        {
            new("userId", userId.ToString()),
            new(ClaimTypes.Role, role.ToString())
        };
        
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = issuer,
            Audience = audience,
            Expires = expires,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!)),
                SecurityAlgorithms.HmacSha256Signature)
        };
        
        var securityToken = tokenHandler.CreateToken(descriptor);
        return tokenHandler.WriteToken(securityToken);
    }
}