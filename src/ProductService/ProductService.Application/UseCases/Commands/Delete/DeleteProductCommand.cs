using FluentValidation;
using MediatR;
using ProductService.Domain.Repositories;
using Shared.CQRS;
using Shared.Exceptions;

namespace ProductService.Application.UseCases.Commands.Delete;

public sealed record DeleteProductCommand(Guid Productid) 
    : ICommand;

public class DeleteProductCommandValidator 
    : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Productid)
            .NotEmpty().WithMessage("ProductId Id is required for deletion");
    }
}
public class DeleteProductCommandHandler(IProductRepository productRepository) 
    : ICommandHandler<DeleteProductCommand>
{
    public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken ct)
    {
        var user = await productRepository.GetByIdAsync(command.Productid, ct);

        if (user is null)
        {
            throw new NotFoundException("Product", command.Productid);
        }

        await productRepository.DeleteAsync(user, ct);
        
        return Unit.Value;
    }
}