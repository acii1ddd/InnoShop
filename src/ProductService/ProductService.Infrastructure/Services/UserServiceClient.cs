using System.Net;
using System.Text.Json;
using ProductService.Application.ExternalDtos;
using ProductService.Application.Interfaces;

namespace ProductService.Infrastructure.Services;

public class UserServiceClient(HttpClient httpClient) 
    : IUserServiceClient
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    public async Task<bool> ExistsAsync(Guid userId, CancellationToken ct)
    {
        var response = await httpClient.GetAsync($"{userId}", ct);

        return response.StatusCode == HttpStatusCode.OK;
    }
    
    public async Task<UserResponseDto?> GetByIdAsync(Guid userId, CancellationToken ct)
    {
        var response = await httpClient.GetAsync($"{userId}", ct);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        await using var stream = await response.Content.ReadAsStreamAsync(ct);

        var user = await JsonSerializer.DeserializeAsync<UserResponseDto>(
            stream,
            JsonOptions,
            ct
        );

        return user;
    }
    
    public async Task<List<UserResponseDto>> GetAllAsync(IEnumerable<Guid> userIds, CancellationToken ct)
    {
        var userIdsList = userIds.ToList();
        if (userIdsList.Count == 0)
        {
            return [];
        }

        var tasks = userIdsList
            .Select(userId => GetByIdAsync(userId, ct));
        
        var results = await Task.WhenAll(tasks);
        
        return results
            .Where(u => u is not null)
            .Select(u => u!)
            .ToList();
    }
}
