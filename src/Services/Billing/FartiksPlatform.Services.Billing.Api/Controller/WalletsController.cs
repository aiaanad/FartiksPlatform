using System.Security.Claims;
using FartiksPlatform.Services.Billing.Application.Interfaces;
using FartiksPlatform.Services.Billing.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FartiksPlatform.Services.Billing.Api.Controller;

[ApiController]
[Route("api/v1/wallets")]
[Authorize]
public class WalletsController : ControllerBase
{
    private readonly IWalletRepository _walletRepository;

    public WalletsController(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyBalances()
    {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst("sub")?.Value;

        if (!Guid.TryParse(userIdClaim, out Guid playerId)) return Unauthorized();

        var goldWallet = await _walletRepository.GetByPlayerAndCurrencyAsync(playerId, CurrencyType.Gold);
        var diamondWallet = await _walletRepository.GetByPlayerAndCurrencyAsync(playerId, CurrencyType.Diamond);

        var response = new[]
        {
            new { currency = "Gold", balance = goldWallet?.Balance ?? 0m },
            new { currency = "Diamond", balance = goldWallet?.Balance ?? 0m }
        };
        
        return Ok(response);
    }
}
