using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FartiksPlatform.Services.Gameplay.Domain.Entities;

namespace FartiksPlatform.Services.Gameplay.Infrastructure.Persistence.Configurations;

public class GameRoundConfiguration : IEntityTypeConfiguration<GameRound>
{
    public void Configure(EntityTypeBuilder<GameRound> builder)
    {
        builder.ToTable("GameRounds", "gameplay");

        builder.HasKey(gr => gr.Id);

        builder.Property(gr => gr.Id)
            .HasColumnName("GameRoundId")
            .IsRequired()
            .HasDefaultValueSql("NEWID()");

        builder.Property(gr => gr.PlayerId)
            .HasColumnName("PlayerId")
            .IsRequired();

        builder.Property(gr => gr.GameId)
            .HasColumnName("GameId")
            .IsRequired();

        builder.Property(gr => gr.BetAmount)
            .HasColumnName("BetAmount")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(gr => gr.OutcomeJson)
            .HasColumnName("OutcomeJson")
            .HasColumnType("NVARCHAR(MAX)")
            .IsRequired(false);

        builder.Property(gr => gr.Result)
            .HasColumnName("Result")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(gr => gr.PayoutAmount)
            .HasColumnName("PayoutAmount")
            .HasPrecision(18, 2)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(gr => gr.PlayedAt)
            .HasColumnName("PlayedAt")
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasIndex(gr => gr.PlayerId)
            .HasDatabaseName("IX_GameRounds_PlayerId");

        builder.HasIndex(gr => gr.GameId)
            .HasDatabaseName("IX_GameRounds_GameId");

        builder.HasIndex(gr => gr.PlayedAt)
            .HasDatabaseName("IX_GameRounds_PlayedAt");

        builder.HasIndex(gr => new { gr.PlayerId, gr.PlayedAt })
            .HasDatabaseName("IX_GameRounds_PlayerId_PlayedAt");

        builder.HasIndex(gr => new { gr.PlayerId, gr.Result })
            .HasDatabaseName("IX_GameRounds_PlayerId_Result");

        builder.HasIndex(gr => new { gr.GameId, gr.PlayedAt })
            .HasDatabaseName("IX_GameRounds_GameId_PlayedAt");

        builder.HasOne<Game>()
            .WithMany()
            .HasForeignKey(gr => gr.GameId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_GameRounds_Games");

        builder.Property(gr => gr.Result)
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}
