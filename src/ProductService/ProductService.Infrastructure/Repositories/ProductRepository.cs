using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repositories;

public class ProductRepository(ProductContext context) 
    : IProductRepository
{
    public async Task<IReadOnlyList<ProductEntity>> GetAllAsync(
        Expression<Func<ProductEntity, bool>> predicate, CancellationToken ct)
    {
        return await context.Products
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync(cancellationToken: ct);
    }

    public async Task<ProductEntity?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await context.Products
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: ct);
    }

    public async Task<Guid> AddAsync(ProductEntity entity, CancellationToken ct)
    {
        await context.Products.AddAsync(entity, ct);

        await context.SaveChangesAsync(ct);
        
        return entity.Id;
    }

    public async Task UpdateAsync(ProductEntity entity, CancellationToken ct)
    {
        context.Products.Update(entity);

        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(ProductEntity entity, CancellationToken ct)
    {
        context.Products.Remove(entity);

        await context.SaveChangesAsync(ct);
    }
}