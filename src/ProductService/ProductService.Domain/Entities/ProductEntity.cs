namespace ProductService.Domain.Entities;

public class ProductEntity
{
    public Guid Id { get; private set; }
    
    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public bool IsAvailable { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public static ProductEntity Create(Guid id, string name, string description, decimal price, Guid userId)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty", nameof(id));

        ArgumentException.ThrowIfNullOrEmpty(name);
        if (name.Length < 3)
            throw new ArgumentException("Name must be at least 3 characters long.", nameof(name));

        ArgumentException.ThrowIfNullOrEmpty(description);
        if (description.Length < 10)
            throw new ArgumentException("Description must be at least 10 characters long.", nameof(description));

        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero.", nameof(price));

        if (userId == Guid.Empty)
            throw new ArgumentException("UserId cannot be empty", nameof(userId));

        return new ProductEntity
        {
            Id = id,
            Name = name,
            Description = description,
            Price = price,
            IsAvailable = true,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void ChangeName(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        if (name.Length < 3)
            throw new ArgumentException("Name must be at least 3 characters long.", nameof(name));

        Name = name;
    }

    public void ChangeDescription(string description)
    {
        ArgumentException.ThrowIfNullOrEmpty(description);
        if (description.Length < 10)
            throw new ArgumentException("Description must be at least 10 characters long.", nameof(description));

        Description = description;
    }

    public void ChangePrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero.", nameof(price));

        Price = price;
    }

    public void MarkAvailable()
    {
        IsAvailable = true;
    }

    public void MarkUnavailable()
    {
        IsAvailable = false;
    }
}