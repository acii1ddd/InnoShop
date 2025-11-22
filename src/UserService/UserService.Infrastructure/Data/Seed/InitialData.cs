using UserService.Domain.Entities;
using UserService.Domain.Enums;

namespace UserService.Infrastructure.Data.Seed;

internal static class InitialData
{
    public static IEnumerable<UserEntity> Users =>
    [
        UserEntity.Create(
            Guid.NewGuid(), 
            "Alice Johnson",
            "alice.johnson@example.com",
            UserRole.Admin,
            "hashed_password_1234567890",
            isActive: true,
            isEmailConfirmed: true
        ),
        UserEntity.Create(
            Guid.Parse("22222222-2222-2222-2222-222222222222"),
            "Bob Smith",
            "bob.smith@example.com",
            UserRole.Default,
            "hashed_password_0987654321",
            isActive: false,
            isEmailConfirmed: false
        ),
        UserEntity.Create(
            Guid.Parse("33333333-3333-3333-3333-333333333333"),
            "Charlie Brown",
            "charlie.brown@example.com",
            UserRole.Default,
            "hashed_password_abcdef123456",
            isActive: false,
            isEmailConfirmed: false
        )
    ];
}