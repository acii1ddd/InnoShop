using System.Net;
using ProductService.Application.Interfaces;

namespace ProductService.Infrastructure.Services;

public class UserServiceClient(HttpClient httpClient) 
    : IUserServiceClient
{
    public async Task<bool> ExistsAsync(Guid userId, CancellationToken ct)
    {
        var response = await httpClient.GetAsync($"{userId}", ct);

        return response.StatusCode == HttpStatusCode.OK;
    }
    
    // public async Task<UserResponseDto> GetByIdAsync(Guid userId, CancellationToken ct)
    // {
    //     var response = await httpClient.GetAsync($"{userId}", ct);
    //     
    //     var stream = await response.Content.ReadAsStreamAsync(ct);
    //
    //     var user = await JsonSerializer.DeserializeAsync<UserResponseDto>(
    //         stream,
    //         new JsonSerializerOptions { PropertyNameCaseInsensitive = true },
    //         ct
    //     );
    //     
    //     return user;
    // }
}