using Shared.CQRS;
using Shared.Exceptions;
using UserService.Application.Dtos;
using UserService.Domain.Interfaces;
using UserService.Domain.Interfaces.Auth;
using UserService.Domain.Repositories;

namespace UserService.Application.UseCases.Commands.Auth;

public sealed record LoginUserCommand(string Email, string Password) 
    : ICommand<LoginUserResult>;

public sealed record LoginUserResult(LoginUserDto User);

internal sealed class LoginUserCommandHandler(
    ITokenGenerator tokenGenerator, 
    IUserRepository userRepository, 
    IPasswordHasher passwordHasher) 
        : ICommandHandler<LoginUserCommand, LoginUserResult>
{
    public async Task<LoginUserResult> Handle(
        LoginUserCommand command, CancellationToken ct)
    {
        var user = await userRepository.GetByEmailAsync(command.Email, ct);

        if (user is null)
        {
            throw new NotFoundException("User", command.Email);
        }
        
        if (!passwordHasher.Verify(command.Password, user.PasswordHash))
        {
            throw new BadRequestException("Incorrect credentials");
        }
        
        if (!user.IsEmailConfirmed)
        {
            throw new BadRequestException("User email is not confirmed");
        }

        var accessToken = tokenGenerator
            .GenerateAccessToken(user.Id, user.Role);

        var loginUserDto = new LoginUserDto(user.Id, user.Role.ToString(), accessToken);
        return new LoginUserResult(loginUserDto);
    }
}