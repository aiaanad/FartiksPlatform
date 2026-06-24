using FartiksPlatform.Services.User.Domain.Entities;
using FartiksPlatform.Services.User.Domain.Repositories;
using FartiksPlatform.Services.User.Infrastructure.Persistence.Configurations;

namespace FartiksPlatform.Services.User.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IUserDbContext _context;

    public UserRepository(IUserDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<AppUser?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<AppUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<(IReadOnlyList<AppUser> Users, int TotalCount)> GetUsersPagedAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task AddUserAsync(AppUser user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateUserAsync(AppUser user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
