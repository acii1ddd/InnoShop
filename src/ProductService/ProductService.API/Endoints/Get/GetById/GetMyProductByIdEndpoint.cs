using Mapster;
using MediatR;
using ProductService.API.EndpointsSettings;
using ProductService.Application.Dtos;
using ProductService.Application.UseCases.Queries.GetById;
using ProductService.Domain.Interfaces;

namespace ProductService.API.Endoints.Get.GetById;

public sealed record GetMyProductByIdResponse(ProductInfoDto Product);

public class GetMyProductByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("my/products/{productId:guid}", async (
                Guid productId,
                ISender sender,
                IUserContext userContext,
                CancellationToken ct) =>
            {
                var userId = userContext.GetUserId();
                
                var result = await sender.Send(new GetMyProductByIdQuery(productId, userId), ct);

                var response = result.Adapt<GetMyProductByIdResponse>();
                
                return Results.Ok(response);
            })
            .WithName("GetMyProductById")
            .Produces<GetMyProductByIdResponse>()
            .WithSummary("Get my product info by specified id")
            .AllowAnonymous();
    }
}