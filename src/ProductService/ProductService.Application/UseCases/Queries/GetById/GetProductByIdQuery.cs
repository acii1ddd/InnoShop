using ProductService.Application.Dtos;
using ProductService.Application.Interfaces;
using ProductService.Domain.Repositories;
using Shared.CQRS;
using Shared.Exceptions;

namespace ProductService.Application.UseCases.Queries.GetById;

public sealed record GetProductByIdQuery(Guid Id) 
    : IQuery<GetProductByIdResult>;

public sealed record GetProductByIdResult(ProductWithUserDto Product);

internal class GetProductByIdQueryHandler(
    IProductRepository productRepository, 
    IUserServiceClient userServiceClient)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken ct)
    {
        var product = await productRepository.GetByIdAsync(query.Id, ct);

        if (product is null)
        {
            throw new NotFoundException("Product", query.Id);
        }
        
        if (!product.IsAvailable)
        {
            throw new UnavailableProductException("This product is unavailable", 
                $"ProductId: {product.Id}, Name: {product.Name}");
        }

        var userDto = await userServiceClient.GetByIdAsync(product.UserId, ct);

        if (userDto is null)
        {
            throw new BadRequestException("User associated with this product was not found");
        }
        
        var productDto = new ProductInfoDto(product.Id, product.Name, product.Description, product.Price, product.CreatedAt);
        var userInfo = new UserInfoDto(userDto.User.Id, userDto.User.Name, userDto.User.Email);
        var productWithUserDto = new ProductWithUserDto(userInfo, productDto);
        
        var result = new GetProductByIdResult(productWithUserDto);
        return result;
    }
}