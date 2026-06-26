using Microsoft.EntityFrameworkCore;
using FartiksPlatform.Services.Gameplay.Domain.Repositories;
using FartiksPlatform.Services.Gameplay.Domain.Entities;
using FartiksPlatform.Services.Gameplay.Infrastructure.Persistence;

namespace FartiksPlatform.Services.Gameplay.Infrastructure.Persistence.Repositories;

public class GameRepository : IGameRepository
{
    private readonly GameplayDbContext _context;
    private readonly DbSet<Game> _games;

    public GameRepository(GameplayDbContext context)
    {
        _context = context;
        _games = context.Games;
    }

    public async Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _games
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
    }

    public async Task<Game?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _games
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<Game>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _games
            .AsNoTracking()
            .OrderBy(g => g.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Game>> GetByTypeAsync(string type, CancellationToken cancellationToken = default)
    {
        return await _games
            .AsNoTracking()
            .Where(g => g.Type == type)
            .OrderBy(g => g.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _games.AnyAsync(g => g.Id == id, cancellationToken);
    }

    public async Task AddAsync(Game game, CancellationToken cancellationToken = default)
    {
        await _games.AddAsync(game, cancellationToken);
    }

    public void Update(Game game)
    {
        _games.Update(game);
    }

    public void Delete(Game game)
    {
        _games.Remove(game);
    }
}
