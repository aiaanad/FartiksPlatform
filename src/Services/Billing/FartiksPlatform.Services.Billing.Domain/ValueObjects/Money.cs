using FartiksPlatform.Services.Billing.Domain.Enums;

namespace FartiksPlatform.Services.Billing.Domain.ValueObjects;

public record Money(decimal Amount, CurrencyType Currency);
