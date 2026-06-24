using Billing.Application.Interfaces;

namespace Billing.Application.Services;

public class WalletService : IWalletService
{
    public Task CreateWalletAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
