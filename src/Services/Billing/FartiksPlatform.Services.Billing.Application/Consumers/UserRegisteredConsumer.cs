using FartiksPlatform.BuildingBlocks.Events;
using FartiksPlatform.Services.Billing.Application.Interfaces;
using FartiksPlatform.Services.Billing.Domain.Entities;
using FartiksPlatform.Services.Billing.Domain.Enums;
using MassTransit;

namespace FartiksPlatform.Services.Billing.Application.Consumers;

public class UserRegisteredConsumer : IConsumer<UserRegisteredEvent>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionRepository _transactionRepository;

    public UserRegisteredConsumer(
        IWalletRepository walletRepository,
        IUnitOfWork unitOfWork,
        ITransactionRepository transactionRepository)
    {
        _walletRepository = walletRepository;
        _unitOfWork = unitOfWork;
        _transactionRepository = transactionRepository;
    }

    public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
    {
        UserRegisteredEvent message = context.Message;

        var wallet = new Wallet(
            playerId: message.PlayerId,
            initialBalance: 500m,
            currency: CurrencyType.Gold);

        var transaction = Transaction.Create(
            playerId: message.PlayerId,
            type: TransactionType.DEPOSIT,
            amount: 500m,
            balanceBefore: 0m,
            balanceAfter: 500m,
            sourceId: null,
            sourceType: SourceType.ADMIN_ACTION);

        await _walletRepository.AddAsync(wallet);
        await _transactionRepository.AddAsync(transaction);
        await _unitOfWork.SaveChangesAsync();
    }
}
