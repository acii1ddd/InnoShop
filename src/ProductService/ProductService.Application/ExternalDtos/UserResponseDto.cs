namespace ProductService.Application.ExternalDtos;

public sealed record UserDto(Guid Id, string Name, string Email, string Role);

public record UserResponseDto(UserDto User);