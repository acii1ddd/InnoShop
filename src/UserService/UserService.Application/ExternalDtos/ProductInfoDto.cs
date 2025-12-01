namespace UserService.Application.ExternalDtos;

public sealed record ProductInfoDto(Guid Id, string Name, string Description, 
    decimal Price, DateTime CreatedAt);