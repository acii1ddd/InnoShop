using FluentValidation;
using MediatR;
using Shared.CQRS;
using Shared.Exceptions;
using UserService.Domain.Interfaces;
using UserService.Domain.Repositories;

namespace UserService.Application.UseCases.Commands;

public sealed record UpdateUserPasswordCommand(Guid Id, string Password) 
    : ICommand;

public class UpdateUserPasswordCommandValidator 
    : AbstractValidator<UpdateUserRoleCommand>
{
    public UpdateUserPasswordCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("User Id is required for password update");
    }
}
public class UpdateUserPasswordCommandHandler(
    IUserRepository userRepository, 
    IPasswordHasher passwordHasher)
        : ICommandHandler<UpdateUserPasswordCommand>
{
    public async Task<Unit> Handle(UpdateUserPasswordCommand command, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(command.Id, ct);

        if (user is null)
        {
            throw new NotFoundException("User", command.Id);
        }
        
        var passwordHash = passwordHasher.Hash(command.Password);
        user.ChangePassword(passwordHash);
        
        await userRepository.UpdateAsync(user, ct);
        
        return Unit.Value;
    }
}