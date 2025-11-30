namespace ProductService.Application.Dtos;

public sealed record UserResponseDto(Guid Id, string Name, string Email, string Role);