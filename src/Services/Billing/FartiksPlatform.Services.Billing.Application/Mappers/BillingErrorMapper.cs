using FartiksPlatform.BuildingBlocks.Errors;
using FartiksPlatform.Services.Billing.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace FartiksPlatform.Services.Billing.Application.Mappers;

public class BillingErrorMapper : IErrorMapper
{
    public (int StatusCode, string ErrorType, string Title)? Map(Exception exception)
    {
        return exception switch
        {
            InsufficientFundsException => (
                StatusCodes.Status422UnprocessableEntity,
                ErrorTypes.InsufficientFunds,
                "Недостаточно средств"),

            WalletNotFoundException => (
                StatusCodes.Status404NotFound,
                ErrorTypes.WalletNotFound,
                "Кошелек не найден"
            ),

            _ => null
        };
    }
}
