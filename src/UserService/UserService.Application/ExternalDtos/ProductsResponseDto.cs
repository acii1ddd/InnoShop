namespace UserService.Application.ExternalDtos;

public sealed record ProductsResponseDto(IReadOnlyList<ProductWithUserResponseDto> Products);