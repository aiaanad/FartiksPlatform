using User.Domain.Entities;

namespace User.Application.Interfaces;

public interface IPlayerRepository
{
    Task<Player?> GetByIdAsync(Guid id);
    Task<Player?> GetByUsernameAsync(string username);
    Task AddAsync(Player player);
    Task UpdateAsync(Player player);
}
