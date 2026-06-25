using FartiksPlatform.Services.User.Domain.Entities;
using FartiksPlatform.Services.User.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FartiksPlatform.Services.User.Infrastructure.Persistence;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    { }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Исправлено: .ToList() чтобы не модифицировать коллекцию во время итерации
        var entries = ChangeTracker.Entries()
            .Where(e =>
            {
                return e.Entity is AppUser &&
                                        (e.State == EntityState.Modified || e.State == EntityState.Added);
            })
            .ToList();

        foreach (EntityEntry? entry in entries)
        {
            if (entry.State == EntityState.Modified)
            {
                ((AppUser)entry.Entity).UpdatedAt = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
