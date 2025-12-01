using FluentValidation;
using ProductService.Application.Dtos;
using ProductService.Application.Interfaces;
using ProductService.Domain.Repositories;
using Shared.CQRS;

namespace ProductService.Application.UseCases.Queries.GetAll;

public sealed record GetProductsQuery(
    Guid? UserId,          // owner
    string? Name,          // productName
    decimal? MinPrice,     
    decimal? MaxPrice,
    DateTime? CreatedAfter,
    DateTime? CreatedBefore,
    bool IncludeUnavailable
) : IQuery<GetProductsResult>;

public sealed record GetProductsResult(IReadOnlyList<ProductWithUserDto> Products);

public sealed class GetProductsQueryValidator 
    : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        RuleFor(q => q.MinPrice)
            .GreaterThanOrEqualTo(0)
            .When(q => q.MinPrice.HasValue);

        RuleFor(q => q.MaxPrice)
            .GreaterThanOrEqualTo(0)
            .When(q => q.MaxPrice.HasValue);

        RuleFor(q => q.MaxPrice)
            .GreaterThanOrEqualTo(q => q.MinPrice!.Value)
            .When(q => q.MinPrice.HasValue && q.MaxPrice.HasValue);

        RuleFor(q => q.CreatedBefore)
            .GreaterThanOrEqualTo(q => q.CreatedAfter!.Value)
            .When(q => q.CreatedAfter.HasValue && q.CreatedBefore.HasValue);

        RuleFor(q => q.Name)
            .MaximumLength(100)
            .When(q => !string.IsNullOrWhiteSpace(q.Name));
    }
}
internal sealed class GetProductsQueryHandler(
    IProductRepository productRepository,
    IUserServiceClient userServiceClient) 
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken ct)
    {
        var products = await productRepository.GetAllAsync(
            p => (query.UserId == null || p.UserId == query.UserId) &&
                 (string.IsNullOrWhiteSpace(query.Name) || p.Name.Contains(query.Name)) &&
                 (query.MinPrice == null || p.Price >= query.MinPrice) &&
                 (query.MaxPrice == null || p.Price <= query.MaxPrice) &&
                 (query.CreatedAfter == null || p.CreatedAt >= query.CreatedAfter) &&
                 (query.CreatedBefore == null || p.CreatedAt <= query.CreatedBefore) &&
                 (query.IncludeUnavailable || p.IsAvailable),
            ct
        );

        // Get unique user IDs
        var uniqueUserIds = products
            .Select(p => p.UserId)
            .Distinct()
            .ToList();

        // Get all users info
        var users = await userServiceClient.GetAllAsync(uniqueUserIds, ct);

        var userDict = users
            .ToDictionary(u => u.User.Id);

        // Map products to ProductWithUserDto
        var productWithUserDtos = products
            .Where(p => userDict.ContainsKey(p.UserId))
            .Select(p =>
            {
                var userResponse = userDict[p.UserId];
                var userInfo = new UserInfoDto(
                    userResponse.User.Id,
                    userResponse.User.Name,
                    userResponse.User.Email
                );
                var productInfo = new ProductInfoDto(
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                    p.CreatedAt
                );
                return new ProductWithUserDto(userInfo, productInfo);
            })
            .OrderByDescending(p => p.ProductInfo.CreatedAt)
            .ToList();

        return new GetProductsResult(productWithUserDtos);
    }
}
