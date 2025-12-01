namespace UserService.Application.ExternalDtos;

public record ProductWithUserResponseDto(UserInfoDto UserInfo, ProductInfoDto ProductInfo);