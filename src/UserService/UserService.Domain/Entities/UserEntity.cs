using UserService.Domain.Enums;

namespace UserService.Domain.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;

    public UserRole Role { get; set; }
    public string PasswordHash { get; set; } =  string.Empty;
    public bool IsActive { get; set; }
    public bool IsEmailConfirmed { get; set; }
}