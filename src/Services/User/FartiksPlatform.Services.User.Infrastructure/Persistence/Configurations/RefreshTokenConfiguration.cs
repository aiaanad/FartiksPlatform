using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FartiksPlatform.Services.User.Domain.Entities;

namespace FartiksPlatform.Services.User.Infrastructure.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens", "auth");
        
        builder.HasKey(rt => rt.Id);
        
        builder.Property(rt => rt.Id)
            .HasDefaultValueSql("NEWID()");
        
        builder.Property(rt => rt.Token)
            .HasMaxLength(500)
            .IsRequired();
        
        builder.HasIndex(rt => rt.Token)
            .IsUnique()
            .HasDatabaseName("UK_RefreshTokens_Token");
        
        builder.Property(rt => rt.ExpiresAt)
            .IsRequired();
        
        builder.Property(rt => rt.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");
        
        builder.Property(rt => rt.RevokedAt)
            .IsRequired(false);
        
        builder.Property(rt => rt.RevokedReason)
            .HasMaxLength(200)
            .IsRequired(false);
        
        builder.Property(rt => rt.DeviceInfo)
            .HasMaxLength(200)
            .IsRequired(false);
        
        builder.Property(rt => rt.IpAddress)
            .HasMaxLength(50)
            .IsRequired(false);
        
        builder.HasIndex(rt => rt.UserId)
            .HasDatabaseName("IX_RefreshTokens_UserId");
        
        builder.HasIndex(rt => new { rt.UserId, rt.ExpiresAt })
            .HasDatabaseName("IX_RefreshTokens_UserId_ExpiresAt");
    }
}