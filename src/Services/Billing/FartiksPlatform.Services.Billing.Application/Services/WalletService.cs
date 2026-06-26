using FartiksPlatform.Services.Billing.Application.Interfaces;

namespace FartiksPlatform.Services.Billing.Application.Services;

public class WalletService : IWalletService
{
    public Task CreateWalletAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
