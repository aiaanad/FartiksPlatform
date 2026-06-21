using Gameplay.Domain.Enums;

namespace Gameplay.Domain.Entities;

public class Game
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public GameType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public string RulesJson { get; set; } = string.Empty;
}
