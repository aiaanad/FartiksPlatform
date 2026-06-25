using FartiksPlatform.Services.User.Domain.Repositories;

namespace FartiksPlatform.Services.User.Application.Abstractions.Persistence;

public interface IUserUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IRefreshTokenRepository RefreshTokens { get; }
}
