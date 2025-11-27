using UserService.Domain.Entities;
using UserService.Domain.Enums;

namespace UserService.Infrastructure.Data.Seed;

internal static class InitialData
{
    public static IEnumerable<UserEntity> Users =>
    [
        UserEntity.Create(
            Guid.Parse("11111111-1111-1111-1111-111111111111"), 
            "Alice Johnson",
            "alice@com",
            UserRole.Admin,
            "$2a$11$AuzifkcqGdSB/hXm4XkVf.fnfdn6bp.9wT2sMqLetOiK7vyIcd8gm",
            isActive: true,
            isEmailConfirmed: true
        ),
        UserEntity.Create(
            Guid.Parse("22222222-2222-2222-2222-222222222222"),
            "Bob Smith",
            "bob.smith@example.com",
            UserRole.Default,
            "hashed_password_0987654321",
            isActive: true,
            isEmailConfirmed: false
        ),
        UserEntity.Create(
            Guid.Parse("33333333-3333-3333-3333-333333333333"),
            "Charlie Brown",
            "charlie.brown@example.com",
            UserRole.Default,
            "hashed_password_abcdef123456",
            isActive: true,
            isEmailConfirmed: false
        )
    ];
}