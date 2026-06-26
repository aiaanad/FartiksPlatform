namespace FartiksPlatform.Services.Gameplay.Application.UseCases.GetGames;

public record GameDto(
    Guid Id,
    string Name,
    string Type,
    string Description
);
