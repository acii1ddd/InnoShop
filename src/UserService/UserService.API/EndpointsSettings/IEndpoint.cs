namespace UserService.API.EndpointsSettings;

public interface IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app);
}