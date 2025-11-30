namespace UserService.Domain.Entities;

public class PasswordReset
{
    public Guid Id { get; private set; }
    
    public Guid UserId { get; private set; }

    public UserEntity User { get; private set; } = null!;
    
    public string Token { get; private set; } = string.Empty;
    
    public DateTime TokenExpiresAt { get; private set; }
    
    public string GeneratedPasswordHash { get; private set; } = string.Empty;
    
    public static PasswordReset Create(Guid userId, string token, DateTime expiresAt, string generatedPasswordHash)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId cannot be empty.", nameof(userId));
        
        ArgumentException.ThrowIfNullOrEmpty(token);
        
        if (expiresAt <= DateTime.UtcNow)
            throw new ArgumentException("Expiration date must be in the future.", nameof(expiresAt));

        return new PasswordReset
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = token,
            TokenExpiresAt = expiresAt,
            GeneratedPasswordHash = generatedPasswordHash
        };
    }
    
    public void ValidateToken()
    {
        if (TokenExpiresAt < DateTime.UtcNow)
            throw new InvalidOperationException("Token has expired.");
    }
}