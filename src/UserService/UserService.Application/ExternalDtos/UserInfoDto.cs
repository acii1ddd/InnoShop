namespace UserService.Application.ExternalDtos;

public record UserInfoDto(Guid UserId, string UserName, 
    string UserEmail);