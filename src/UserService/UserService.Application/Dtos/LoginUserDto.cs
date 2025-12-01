namespace UserService.Application.Dtos;

public sealed record LoginUserDto(Guid UserId, string Role, string AccessToken);