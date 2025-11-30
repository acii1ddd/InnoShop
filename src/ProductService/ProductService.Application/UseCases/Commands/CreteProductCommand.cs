using FluentValidation;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using Shared.CQRS;
using Shared.Exceptions;

namespace ProductService.Application.UseCases.Commands;

public sealed record CreateProductCommand(string Name, string Description, decimal Price, Guid UserId)
    : ICommand<CreateProductResult>;

public sealed record CreateProductResult(Guid UserId);


public class CreteProductValidator 
    : AbstractValidator<CreateProductCommand>
{
    public CreteProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MinimumLength(3).WithMessage("Product name must be at least 3 characters long.")
            .MaximumLength(1024).WithMessage("Product name must not exceed 1024 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Product description is required.")
            .MinimumLength(10).WithMessage("Product description must be at least 10 characters long.")
            .MaximumLength(1024).WithMessage("Product description must not exceed 1024 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}

public class CreteProductCommandHandler(
    IProductRepository productRepository, 
    IUserServiceClient userServiceClient)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken ct)
    {
        var isSuccess = await userServiceClient.ExistsAsync(command.UserId, ct);

        if (!isSuccess)
        {
            throw new BadRequestException("User is not exists in user service database.");
        }
        
        var user = ProductEntity.Create(
            Guid.NewGuid(),
            command.Name,
            command.Description,
            command.Price,
            command.UserId
        );
        
        var userId = await productRepository.AddAsync(user, ct);

        return new CreateProductResult(userId);
    }
}