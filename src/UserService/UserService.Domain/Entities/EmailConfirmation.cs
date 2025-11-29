namespace UserService.Domain.Entities;

public class EmailConfirmation
{
    public Guid Id { get; private set; }
    
    public Guid UserId { get; private set; }

    public UserEntity User { get; private set; } = null!;
    
    public string Token { get; private set; } = string.Empty;
    
    public DateTime TokenExpiresAt { get; private set; }
    
    public static EmailConfirmation Create(Guid userId, string token, DateTime expiresAt)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId cannot be empty.", nameof(userId));
        
        ArgumentException.ThrowIfNullOrEmpty(token);
        
        if (expiresAt <= DateTime.UtcNow)
            throw new ArgumentException("Expiration date must be in the future.", nameof(expiresAt));

        return new EmailConfirmation
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = token,
            TokenExpiresAt = expiresAt,
        };
    }
    
    public void ValidateToken(string token)
    {
        if (TokenExpiresAt < DateTime.UtcNow)
            throw new InvalidOperationException("Token has expired.");
    }
}