using FartiksPlatform.Services.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FartiksPlatform.Services.User.Infrastructure.Persistence.Configurations;

public interface IUserDbContext
{
    DbSet<AppUser> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public class UserDbContext : DbContext, IUserDbContext
{
    public DbSet<AppUser> Users { get; set; } = null!;

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        throw new NotImplementedException();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
