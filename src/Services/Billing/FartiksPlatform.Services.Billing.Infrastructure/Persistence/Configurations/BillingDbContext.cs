using FartiksPlatform.Services.Billing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FartiksPlatform.Services.Billing.Infrastructure.Persistence.Configurations;

public class BillingDbContext : DbContext
{
    public BillingDbContext(DbContextOptions<BillingDbContext> options)
    : base(options) { }

    public DbSet<Wallet> Wallets => Set<Wallet>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Wallet>().Property(w => w.Version).IsRowVersion();
        modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
    }
}
