using UserService.Domain.Enums;

namespace UserService.Domain.Entities;

public class UserEntity
{
    public Guid Id { get; private set; }
    
    public string Name { get; private set; } = string.Empty;
    
    public string Email { get; private set; } = string.Empty;

    public UserRole Role { get; private set; }
    
    public string PasswordHash { get; private set; } =  string.Empty;
    
    public bool IsActive { get; private set; }
    
    public bool IsEmailConfirmed { get; private set; }

    public static UserEntity Create(Guid id, string name, string email, UserRole role, string passwordHash, bool isActive, bool isEmailConfirmed)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id can not by empty", nameof(id));
       
        ArgumentException.ThrowIfNullOrEmpty(name);
        if (name.Length < 5)
            throw new ArgumentException("Name should include at least 5 letters.", nameof(name));
        
        ArgumentException.ThrowIfNullOrEmpty(email);
        if (!email.Contains('@'))
            throw new ArgumentException("Incorrect email.", nameof(email));

        if (!Enum.IsDefined(role))
            throw new ArgumentException("Incorrect user role.", nameof(role));
        
        ArgumentException.ThrowIfNullOrEmpty(passwordHash);
        
        var userEntity = new UserEntity
        {
            Id = id,
            Name = name,
            Email = email,
            Role = role,
            PasswordHash = passwordHash,
            IsActive = isActive,
            IsEmailConfirmed = isEmailConfirmed
        };

        return userEntity;
    }
    
    public void ConfirmEmailAsync()
    {
        if (IsEmailConfirmed)
            throw new InvalidOperationException("Email is already confirmed.");
        
        IsEmailConfirmed = true;
    }
    
    public void Deactivate()
    {
        if (!IsActive)
            throw new InvalidOperationException("User is already not active.");
        
        IsActive = false;
    }

    public void Activate()
    {
        if (IsActive)
            throw new InvalidOperationException("User is already active.");
        
        IsActive = true;
    }
    
    public void ChangeRole(UserRole newRole)
    {
        if (Role == newRole)
            throw new InvalidOperationException("This role is already assigned to this user.");
        
        if (!Enum.IsDefined(newRole))
            throw new ArgumentException("Incorrect user role.", nameof(newRole));
        
        Role = newRole;
    }
    
    public void ChangeName(string name)
    {
        if (name == Name)
            throw new InvalidOperationException("This name is already assigned to this user.");
        
        ArgumentException.ThrowIfNullOrEmpty(name);
        if (name.Length < 5)
            throw new ArgumentException("Name should include at least 5 letters.", nameof(name));
        
        Name = name;
    }
    
    public void ChangeEmail(string email)
    {
        if (email == Email)
            throw new InvalidOperationException("This email is already assigned to this user.");
     
        ArgumentException.ThrowIfNullOrEmpty(email);
        if (!email.Contains('@'))
            throw new ArgumentException("Incorrect email.", nameof(email));
        
        Email = email;
    }
}