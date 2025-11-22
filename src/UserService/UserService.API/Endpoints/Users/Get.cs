namespace UserService.API.Endpoints.Users;

public sealed record GetUsersRequest(int PageIndex = 1, int PageSize = 10);

public sealed record GetUserResponse();

public class Get
{
    
}