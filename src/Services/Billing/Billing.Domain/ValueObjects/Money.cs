using Billing.Domain.Enums;

namespace Billing.Domain.ValueObjects;

public record Money(decimal Amount, CurrencyType Currency);
