using Billing.Domain.Enums;
using Billing.Domain.ValueObjects;

namespace Billing.Domain.Entities;

public class Wallet
{
    public Guid Id { get; set; }
    public Guid PlayerId { get; set; }
    public decimal Balance { get; set; }
    public CurrencyType Currency { get; set; }
    public int Version { get; set; }

    public void Debit(Money money) => throw new NotImplementedException();
    public void Credit(Money money) => throw new NotImplementedException();
}
