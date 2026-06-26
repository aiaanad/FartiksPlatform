using FartiksPlatform.BuildingBlocks.Common;
using FartiksPlatform.BuildingBlocks.Events;
using FartiksPlatform.Services.User.Application.Abstractions.Persistence;
using FartiksPlatform.Services.User.Application.Errors;
using FartiksPlatform.Services.User.Application.Interfaces;
using FartiksPlatform.Services.User.Domain.Entities;
using FartiksPlatform.Services.User.Domain.ValueObjects;

namespace FartiksPlatform.Services.User.Application.Commands.RegisterUser;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Result>
{
    private readonly IUserUnitOfWork _unitOfWork;
    private readonly IPasswordHashGenerator _passwordHashGenerator;
    private readonly IEventPublisher _eventPublisher;

    public RegisterUserCommandHandler(
        IUserUnitOfWork unitOfWork,
        IPasswordHashGenerator passwordHashGenerator,
        IEventPublisher eventPublisher)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _passwordHashGenerator = passwordHashGenerator ?? throw new ArgumentNullException(nameof(passwordHashGenerator));
        _eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        Email email;
        try
        {
            email = Email.Create(request.Email);
        }
        catch (ArgumentException)
        {
            return Result.Failure(UserErrors.InvalidEmail);
        }

        if (await _unitOfWork.Users.GetUserByEmailAsync(email.Value, cancellationToken) is not null)
            return Result.Failure(UserErrors.UserAlreadyExists);

        if (await _unitOfWork.Users.GetUserByUsernameAsync(request.Username, cancellationToken) is not null)
            return Result.Failure(UserErrors.UserAlreadyExists);

        string passwordHash = _passwordHashGenerator.GenerateHash(request.Password);
        var user = AppUser.Create(Guid.NewGuid(), request.Username, email, passwordHash, request.Role);

        _unitOfWork.Users.AddUser(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventPublisher.PublishAsync(
            new UserRegisteredEvent
            {
                PlayerId = user.Id,
                Username = user.Username,
                Email = user.Email.Value
            },
            "user.events",
            "user.registered");

        return Result.Success();
    }
}
