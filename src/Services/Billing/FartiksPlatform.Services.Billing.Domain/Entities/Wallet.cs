using FartiksPlatform.Services.Billing.Domain.Enums;
using FartiksPlatform.Services.Billing.Domain.ValueObjects;

namespace FartiksPlatform.Services.Billing.Domain.Entities;

public class Wallet
{
    public Guid Id { get; private set; }
    public Guid PlayerId { get; set; }
    public decimal Balance { get; set; }
    public CurrencyType Currency { get; set; }
    public int Version { get; set; }

    public Wallet(Guid playerId, decimal initialBalance, CurrencyType currency)
    {
        Id = Guid.NewGuid();
        PlayerId = playerId;
        Balance = initialBalance;
        Currency = currency;
    }
    
    private Wallet() { }

    public void Debit(Money money)
    {
        if (money.Currency != Currency) throw new ArgumentException("Currency mismatch");
        
        if (Balance < money.Amount) throw new InvalidOperationException("Not enough money");
        
        Balance -= money.Amount;
    }

    public void Credit(Money money)
    {
        if (money.Currency != Currency) throw new ArgumentException("Currency mismatch");
        Balance += money.Amount;
    }
}
