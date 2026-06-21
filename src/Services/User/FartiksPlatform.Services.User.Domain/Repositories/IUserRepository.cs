using FartiksPlatform.Services.User.Domain.Entities;

namespace FartiksPlatform.Services.User.Domain.Repositories;

public interface IUserRepository
{
    Task<AppUser?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<AppUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<AppUser?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<AppUser> Users, int TotalCount)> GetUsersPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task AddUserAsync(AppUser user, CancellationToken cancellationToken = default);
    Task UpdateUserAsync(AppUser user, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
