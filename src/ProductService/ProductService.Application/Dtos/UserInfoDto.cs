namespace ProductService.Application.Dtos;

public record UserInfoDto(Guid UserId, string UserName, 
    string UserEmail);