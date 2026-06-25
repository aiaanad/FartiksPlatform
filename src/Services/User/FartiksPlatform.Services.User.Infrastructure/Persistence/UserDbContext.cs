using FartiksPlatform.Services.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FartiksPlatform.Services.User.Infrastructure.Persistence.Configurations;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) 
        : base(options) 
    { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is User && 
                       (e.State == EntityState.Modified || e.State == EntityState.Added));
        
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Modified)
            {
                ((User)entry.Entity).UpdatedAt = DateTime.UtcNow;
            }
        }
        
        return await base.SaveChangesAsync(cancellationToken);
    }
}