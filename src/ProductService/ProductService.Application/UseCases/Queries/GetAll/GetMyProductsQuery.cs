using Mapster;
using ProductService.Application.Dtos;
using ProductService.Domain.Repositories;
using Shared.CQRS;
using Shared.Exceptions;

namespace ProductService.Application.UseCases.Queries.GetAll;

public sealed record GetMyProductsQuery(Guid UserId)
    : IQuery<GetMyProductsResult>;

public sealed record GetMyProductsResult(IReadOnlyList<ProductInfoDto> Products);

internal sealed class GetMyProductsQueryHandler(IProductRepository productRepository)
    : IQueryHandler<GetMyProductsQuery, GetMyProductsResult>
{
    public async Task<GetMyProductsResult> Handle(GetMyProductsQuery query, 
        CancellationToken ct)
    {
        var products = await productRepository.GetAllAsync(
            p => p.UserId == query.UserId,
            ct
        );

        if (products[0].UserId != query.UserId)
        {
            throw new ForbiddenException($"Products does not belong to user {query.UserId}");
        }

        var productDtos = products.Adapt<IReadOnlyList<ProductInfoDto>>();

        var result = new GetMyProductsResult(productDtos);
        return result;
    }
}