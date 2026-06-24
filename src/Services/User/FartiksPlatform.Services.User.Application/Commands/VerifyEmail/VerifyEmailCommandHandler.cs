using FartiksPlatform.BuildingBlocks.Common;
using FartiksPlatform.Services.User.Domain.Repositories;

namespace FartiksPlatform.Services.User.Application.Commands.VerifyEmail;

public class VerifyEmailCommandHandler : ICommandHandler<VerifyEmailCommand, Result>
{
    private readonly IUserRepository _userRepository;

    public VerifyEmailCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<Result> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
