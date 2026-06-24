using FartiksPlatform.BuildingBlocks.Common;
using FartiksPlatform.Services.User.Application.Interfaces;
using FartiksPlatform.Services.User.Domain.Repositories;
using FartiksPlatform.Services.User.Domain.ValueObjects;

namespace FartiksPlatform.Services.User.Application.Commands.RegisterUser;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashGenerator _passwordHashGenerator;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHashGenerator passwordHashGenerator,
        IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _passwordHashGenerator = passwordHashGenerator ?? throw new ArgumentNullException(nameof(passwordHashGenerator));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
