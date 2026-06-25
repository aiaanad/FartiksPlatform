namespace FartiksPlatform.Services.Billing.Application.Interfaces;

public interface IWalletService
{
    Task CreateWalletAsync(Guid userId, CancellationToken cancellationToken = default);
}
