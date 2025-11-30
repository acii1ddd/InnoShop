using ProductService.API.EndpointsSettings;

namespace ProductService.API.Endoints.Get.GetById;

// public class GetProductWithUserByIdEndpoint : IEndpoint
// {
//     public void MapEndpoint(IEndpointRouteBuilder app)
//     {
//         app.MapGet("my/products/{productId:guid}", async (
//                 Guid productId,
//                 ISender sender,
//                 IUserContext userContext,
//                 CancellationToken ct) =>
//             {
//                 var userId = userContext.GetUserId();
//                 var result = await sender.Send(new GetMyProductByIdQuery(productId, userId), ct);
//                 return Results.Ok(result);
//             })
//             .RequireAuthorization("Default");
//     }
// }