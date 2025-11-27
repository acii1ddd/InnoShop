using UserService.Domain.Enums;

namespace UserService.Application.Dtos;

public sealed record LoginUserDto(Guid UserId, UserRole Role, string AccessToken);