using Grpc.Core;
using BuildingBlocks.Grpc;
using FartiksPlatform.Services.Billing.Application.Interfaces;
using FartiksPlatform.Services.Billing.Domain.Entities;
using FartiksPlatform.Services.Billing.Domain.Enums;
using FartiksPlatform.Services.Billing.Domain.ValueObjects;
using FartiksPlatform.Services.Billing.Domain.Exceptions;
using System.Globalization;

namespace FartiksPlatform.Services.Billing.Api.Grpc;

public class BillingService : BillingGrpcService.BillingGrpcServiceBase
{
    private readonly IWalletRepository _walletRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BillingService(
        IWalletRepository walletRepository,
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork)
    {
        _walletRepository = walletRepository;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
    }

    public override async Task<BetResponse> CheckAndWithdrawBet(BetRequest request, ServerCallContext context)
    {
        try
        {
            var playerId = Guid.Parse(request.PlayerId);
            var gameId = Guid.Parse(request.GameId);
            decimal amount = decimal.Parse(request.Amount, CultureInfo.InvariantCulture);
            var currency = Enum.Parse<CurrencyType>(request.Currency);

            Wallet wallet = await _walletRepository.GetByPlayerAndCurrencyAsync(playerId, currency) ??
                            throw new WalletNotFoundException();

            decimal balanceBefore = wallet.Balance;

            wallet.Debit(new Money(amount, currency));

            var transaction = Transaction.Create(
                playerId: playerId,
                type: TransactionType.BET,
                amount: amount,
                balanceBefore: balanceBefore,
                balanceAfter: wallet.Balance,
                sourceId: gameId,
                sourceType: SourceType.GAME_ROUND);

            await _transactionRepository.AddAsync(transaction);
            await _unitOfWork.SaveChangesAsync();

            return new BetResponse { IsSuccess = true, TransactionId = transaction.Id.ToString() };
        }
        catch (InsufficientFundsException)
        {
            return new BetResponse { IsSuccess = false, ErrorCode = "INSUFFICIENT_FUNDS" };
        }
        catch (Exception)
        {
            return new  BetResponse { IsSuccess = false, ErrorCode = "INTERNAL_ERROR" };
        }
    }

    public override async Task<WinResponse> CreditWin(WinRequest request, ServerCallContext context)
    {
        var playerId = Guid.Parse(request.PlayerId);
        var gameId = Guid.Parse(request.GameId);
        decimal amount = decimal.Parse(request.Amount, CultureInfo.InvariantCulture);
        var currency = Enum.Parse<CurrencyType>(request.Currency);

        Wallet wallet = await _walletRepository.GetByPlayerAndCurrencyAsync(playerId, currency) ??
                        throw new WalletNotFoundException();

        decimal balanceBefore = wallet.Balance;

        wallet.Credit(new Money(amount, currency));

        var transaction = Transaction.Create(
            playerId: playerId,
            type: TransactionType.WIN,
            amount: amount,
            balanceBefore: balanceBefore,
            balanceAfter: wallet.Balance,
            sourceId: gameId,
            sourceType: SourceType.GAME_ROUND);

        await _transactionRepository.AddAsync(transaction);
        await _unitOfWork.SaveChangesAsync();

        return new WinResponse { TransactionId = transaction.Id.ToString() };
    }
}
