using System.ComponentModel;
using Mapster;
using MediatR;
using UserService.API.EndpointsSettings;
using UserService.Application.Dtos;
using UserService.Application.UseCases.Queries;

namespace UserService.API.Endpoints.Users.Get;

public record GetUsersRequest(
    [property: DefaultValue(1)]
    int PageIndex = 1,
    [property: DefaultValue(10)]
    int PageSize = 10
);

public record GetPaginatedUsersResponse(IEnumerable<UserDto> Users, int TotalCount, int TotalPages);

public class GetUsersEndpoint : IEndpoint
{
    public async void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/", async (
                ISender sender,
                [AsParameters] GetUsersRequest getUsersRequest,
                CancellationToken ct) =>
            {
                var query = getUsersRequest.Adapt<GetUsersQuery>();

                var result = await sender.Send(query, ct);

                var response = result.Adapt<GetPaginatedUsersResponse>();
                return Results.Ok(response);
            })
            .WithName("GetUsers")
            .Produces<GetPaginatedUsersResponse>()
            .WithSummary("Get a paginated list of users")
            .RequireAuthorization()
            .AllowAnonymous();
        //.RequireAuthorization("Admin"); todo
    }
}