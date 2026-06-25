using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FartiksPlatform.Services.Gameplay.Domain.Entities;
using FartiksPlatform.Services.Gameplay.Domain.Constants;

namespace FartiksPlatform.Services.Gameplay.Infrastructure.Persistence.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Games", "gameplay");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
            .HasColumnName("GameId")
            .IsRequired()
            .HasDefaultValueSql("NEWID()");

        builder.Property(g => g.Name)
            .HasColumnName("Name")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(g => g.Type)
            .HasColumnName("Type")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(g => g.Description)
            .HasColumnName("Description")
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(g => g.RulesJson)
            .HasColumnName("RulesJson")
            .HasColumnType("NVARCHAR(MAX)")
            .IsRequired(false);

        builder.HasIndex(g => g.Name)
            .HasDatabaseName("IX_Games_Name");

        builder.HasIndex(g => g.Type)
            .HasDatabaseName("IX_Games_Type");

        builder.HasIndex(g => new { g.Type, g.Name })
            .HasDatabaseName("IX_Games_Type_Name");

        builder.HasIndex(g => g.Name)
            .IsUnique()
            .HasDatabaseName("UK_Games_Name");

        builder.HasData(
            new Game
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Classic Slots",
                Type = GameType.Slots,
                Description = "Classic 3-reel slot machine",
                RulesJson = @"{""reels"":3,""paylines"":1,""symbols"":[""CHERRY"",""LEMON"",""ORANGE"",""BELL"",""DIAMOND""]}"
            },
            new Game
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "European Roulette",
                Type = GameType.Roulette,
                Description = "European roulette with single zero",
                RulesJson = @"{""numbers"":37,""colors"":[""RED"",""BLACK"",""GREEN""]}"
            },
            new Game
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Blackjack",
                Type = GameType.Blackjack,
                Description = "Classic 21 card game",
                RulesJson = @"{""decks"":6,""surrender"":true,""doubleAfterSplit"":true}"
            }
        );
    }
}
