using ProductService.Domain.Interfaces;

namespace ProductService.Infrastructure.Services;

public class UserServiceClient(HttpClient httpClient) 
    : IUserServiceClient
{
    public async Task<bool> GetByIdAsync(Guid userId, CancellationToken ct)
    {
        var response = await httpClient.GetAsync($"{userId}", ct);
        return response.IsSuccessStatusCode;
    }
}