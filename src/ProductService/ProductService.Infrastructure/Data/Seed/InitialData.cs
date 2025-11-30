using ProductService.Domain.Entities;

namespace ProductService.Infrastructure.Data.Seed;

internal class InitialData
{
    public static IEnumerable<ProductEntity> Products =>
    [
        ProductEntity.Create(
            Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            "Laptop Pro 15",
            "High performance laptop with 15-inch display, 16GB RAM, 512GB SSD.",
            1499.99m,
            Guid.Parse("11111111-1111-1111-1111-111111111111") // Alice Johnson
        ),
        ProductEntity.Create(
            Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            "Wireless Headphones",
            "Noise-cancelling over-ear headphones with 30 hours battery life.",
            299.99m,
            Guid.Parse("22222222-2222-2222-2222-222222222222") // Bob Smith
        ),
        ProductEntity.Create(
            Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
            "Smartphone X",
            "Latest generation smartphone with OLED display and 128GB storage.",
            999.99m,
            Guid.Parse("33333333-3333-3333-3333-333333333333") // Charlie Brown
        ),
        ProductEntity.Create(
            Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
            "Gaming Mouse",
            "Ergonomic gaming mouse with customizable RGB lighting and 6 programmable buttons.",
            79.99m,
            Guid.Parse("22222222-2222-2222-2222-222222222222") // Bob Smith
        )
    ];
}