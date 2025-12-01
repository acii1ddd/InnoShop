using System.Net.Http.Json;
using UserService.Application.ExternalDtos;
using UserService.Application.Interfaces;

namespace UserService.Infrastructure.Services;

public class ProductServiceClient(HttpClient httpClient) 
    : IProductServiceClient
{
    public async Task<bool> MarkProductAsAvailableAsync(Guid productId, CancellationToken ct)
    {
        var response = await httpClient
            .PutAsync($"{productId}/available", null, ct);

        return response.IsSuccessStatusCode;
    }
    
    public async Task<bool> MarkProductAsUnavailableAsync(Guid productId, CancellationToken ct)
    {
        var response = await httpClient
            .PutAsync($"{productId}/unavailable", null, ct);

        return response.IsSuccessStatusCode;
    }

    public async Task<IReadOnlyList<ProductWithUserResponseDto>> GetAvailableProductsByUserIdAsync(
        Guid userId,
        CancellationToken ct)
    {
        var response = await httpClient
            .GetAsync($"?userId={userId}&includeUnavailable=false", ct);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ProductsResponseDto>(
            cancellationToken: ct);

        return result?.Products ?? [];
    }
    
    public async Task<IReadOnlyList<ProductWithUserResponseDto>> GetUnavailableProductsByUserIdAsync(
        Guid userId,
        CancellationToken ct)
    {
        var response = await httpClient
            .GetAsync($"?userId={userId}&includeUnavailable=true", ct);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ProductsResponseDto>(
            cancellationToken: ct);

        return result?.Products ?? [];
    }
}
