using Mapster;
using MediatR;
using ProductService.API.EndpointsSettings;
using ProductService.Application.Dtos;
using ProductService.Application.UseCases.Queries.GetAll;
using ProductService.Domain.Interfaces;

namespace ProductService.API.Endpoints.Get.GetAll;

public sealed record GetMyProductsByIdResponse(IReadOnlyList<ProductInfoDto> Products);

// todo фильтрация 
public class GetMyProductsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("my/products", async (
            ISender sender,
            IUserContext userContext,
            CancellationToken ct) =>
        {
            var userId = userContext.GetUserId();

            var result = await sender.Send(new GetMyProductsQuery(userId), ct);

            var response = result.Adapt<GetMyProductsByIdResponse>();
            return response;
        })
        .WithName("GetMyProductsById")
        .Produces<GetMyProductsByIdResponse>()
        .WithSummary("Get a list of my products info")
        .RequireAuthorization("Default");
    }
}