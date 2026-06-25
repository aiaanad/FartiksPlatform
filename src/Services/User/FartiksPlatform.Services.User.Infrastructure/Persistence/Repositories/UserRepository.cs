using FartiksPlatform.Services.User.Domain.Entities;
using FartiksPlatform.Services.User.Domain.Repositories;
using FartiksPlatform.Services.User.Infrastructure.Persistence;

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
        return await _context.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task<AppUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
    }

    public async Task<(IReadOnlyList<AppUser> Users, int TotalCount)> GetUsersPagedAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var users = await _context.Users
            .OrderBy(u => u.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var totalCount = await _context.Users.CountAsync();
        return (users, totalCount);
    }

    public async Task AddUserAsync(AppUser user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task UpdateUserAsync(AppUser user, CancellationToken cancellationToken = default)
    {
        await _context.Users.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FindAsync(userId, cancellationToken);
        if (user != null)
        {
            await _context.Users.RemoveAsync(user);
        }
    }
}
