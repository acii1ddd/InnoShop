using ProductService.Application.Dtos;
using ProductService.Domain.Repositories;
using Shared.CQRS;
using Shared.Exceptions;

namespace ProductService.Application.UseCases.Queries.GetById;

public sealed record GetMyProductByIdQuery(Guid Id, Guid UserId) 
    : IQuery<GetMyProductByIdResult>;

public sealed record GetMyProductByIdResult(ProductInfoDto Product);

internal sealed class GetMyProductByIdQueryHandler(IProductRepository productRepository)
    : IQueryHandler<GetMyProductByIdQuery, GetMyProductByIdResult>
{
    public async Task<GetMyProductByIdResult> Handle(
        GetMyProductByIdQuery query, CancellationToken ct)
    {
        var product = await productRepository.GetByIdAsync(query.Id, ct);

        if (product is null)
        {
            throw new NotFoundException("Product", query.Id);
        }
        
        if (product.UserId != query.UserId)
        {
            throw new ForbiddenException($"Product {query.Id} does not belong to user {query.UserId}");
        }

        var productInfoDto = new ProductInfoDto(product.Id, product.Name, product.Description, 
            product.Price, product.CreatedAt);

        var result = new GetMyProductByIdResult(productInfoDto);
        
        return result;
    }
}
