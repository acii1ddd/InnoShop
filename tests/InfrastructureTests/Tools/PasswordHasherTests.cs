using UserService.Infrastructure.Tools;
using Xunit.Abstractions;

namespace InfrastructureTests.Tools;

public class PasswordHasherTests(ITestOutputHelper output)
{
    private readonly PasswordHasher _hasher = new();
    
    [Fact]
    public void Hash_ShouldOutputHash_ForPassword123()
    {
        // Arrange
        var password = "1234";

        // Act
        var hash = _hasher.Hash(password);

        // Output
        output.WriteLine($"Hash for '{password}': {hash}");

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(hash));
        Assert.NotEqual(password, hash);
    }

    [Fact]
    public void Verify_ShouldReturnTrue_ForCorrectPassword()
    {
        // Arrange
        var password = "MySecret123!";
        var hash = _hasher.Hash(password);

        // Act
        var result = _hasher.Verify(password, hash);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Verify_ShouldReturnFalse_ForIncorrectPassword()
    {
        // Arrange
        var password = "MySecret123!";
        var hash = _hasher.Hash(password);

        // Act
        var result = _hasher.Verify("WrongPassword", hash);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Hash_ShouldGenerateDifferentHashes_ForSamePassword()
    {
        // Arrange
        var password = "MySecret123!";

        // Act
        var hash1 = _hasher.Hash(password);
        var hash2 = _hasher.Hash(password);

        // Assert
        Assert.NotEqual(hash1, hash2);
    }
}
