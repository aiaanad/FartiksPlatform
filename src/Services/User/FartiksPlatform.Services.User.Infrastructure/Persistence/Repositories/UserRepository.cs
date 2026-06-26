using Microsoft.EntityFrameworkCore;
using FartiksPlatform.Services.User.Domain.Entities;
using FartiksPlatform.Services.User.Domain.Repositories;
using FartiksPlatform.Services.User.Domain.ValueObjects;
using FartiksPlatform.Services.User.Infrastructure.Persistence;

namespace FartiksPlatform.Services.User.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
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
        // Email — Value Object с конвертацией в строку, сравниваем через .Value
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email.Value == email, cancellationToken);
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
        IOrderedQueryable<AppUser> query = _context.Users.OrderBy(u => u.CreatedAt);
        int totalCount = await query.CountAsync(cancellationToken);
        List<AppUser> users = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (users, totalCount);
    }

    public void AddUser(AppUser user, CancellationToken cancellationToken = default)
    {
        _context.Users.Add(user);
    }

    public void UpdateUser(AppUser user, CancellationToken cancellationToken = default)
    {
        _context.Users.Update(user);
    }

    public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        AppUser? user = await _context.Users.FindAsync(new object[] { userId }, cancellationToken);
        if (user != null)
            _context.Users.Remove(user);
    }
}
