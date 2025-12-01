namespace UserService.Application.Dtos;

public sealed record UserDto(Guid Id, string Name, string Email, string Role);