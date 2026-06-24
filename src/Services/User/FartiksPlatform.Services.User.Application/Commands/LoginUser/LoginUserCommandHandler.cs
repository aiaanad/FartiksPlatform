using FartiksPlatform.BuildingBlocks.Common;
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
        throw new NotImplementedException();
    }
}
