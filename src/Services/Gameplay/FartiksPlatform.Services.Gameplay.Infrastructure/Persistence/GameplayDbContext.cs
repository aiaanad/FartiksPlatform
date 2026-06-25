using Microsoft.EntityFrameworkCore;
using FartiksPlatform.Services.Gameplay.Domain.Entities;

namespace FartiksPlatform.Services.Gameplay.Infrastructure.Persistence;

public class GameplayDbContext : DbContext
{
    public GameplayDbContext(DbContextOptions<GameplayDbContext> options)
        : base(options)
    { }

    public DbSet<Game> Games { get; set; }
    public DbSet<GameRound> GameRounds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameplayDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
