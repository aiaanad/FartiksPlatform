using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FartiksPlatform.Services.User.Domain.Entities;

namespace FartiksPlatform.Services.User.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "auth");
        
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Username)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(u => u.Email)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("UK_Users_Email");
        
        builder.Property(u => u.PasswordHash)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(u => u.Role)
            .HasMaxLength(50)
            .IsRequired()
            .HasDefaultValue("User");
        
        builder.Property(u => u.Status)
            .HasMaxLength(50)
            .IsRequired()
            .HasDefaultValue("Active");
        
        builder.Property(u => u.EmailVerified)
            .IsRequired()
            .HasDefaultValue(false);
        
        builder.Property(u => u.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");
        
        builder.Property(u => u.UpdatedAt)
            .IsRequired(false);
        
        builder.HasMany(u => u.RefreshTokens)
            .WithOne(rt => rt.User)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}