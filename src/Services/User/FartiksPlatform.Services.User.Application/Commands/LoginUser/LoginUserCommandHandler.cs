using FartiksPlatform.BuildingBlocks.Common;
using FartiksPlatform.Services.User.Application.Errors;
using FartiksPlatform.Services.User.Application.Interfaces;
using FartiksPlatform.Services.User.Domain.Repositories;

namespace FartiksPlatform.Services.User.Application.Commands.LoginUser;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, Result<LoginUserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashGenerator _passwordHashGenerator;
    private readonly IJwtProvider _jwtProvider;

    public LoginUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHashGenerator passwordHashGenerator,
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _passwordHashGenerator = passwordHashGenerator ?? throw new ArgumentNullException(nameof(passwordHashGenerator));
        _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
    }

    public async Task<Result<LoginUserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.AppUser? user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);
        if (user is null)
            return Result.Failure<LoginUserResponse>(UserErrors.InvalidCredentials);

        if (!_passwordHashGenerator.VerifyHash(request.Password, user.PasswordHash))
            return Result.Failure<LoginUserResponse>(UserErrors.InvalidCredentials);

        if (user.Status != "Active")
            return Result.Failure<LoginUserResponse>(UserErrors.UserDeactivated);

        string token = _jwtProvider.GenerateToken(user);
        string refreshToken = _jwtProvider.RefreshToken(user);

        var response = new LoginUserResponse(
            user.Id,
            user.Username,
            user.Email.Value,
            token,
            refreshToken);

        return Result.Success(response);
    }
}
