namespace ProductService.Application.Dtos;

public sealed record ProductInfoDto(Guid Id, string Name, string Description, 
    decimal Price, DateTime CreatedAt);