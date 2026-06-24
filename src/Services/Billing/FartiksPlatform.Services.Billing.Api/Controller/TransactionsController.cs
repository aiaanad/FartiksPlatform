using FartiksPlatform.Services.Billing.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FartiksPlatform.Services.Billing.Api.Controller;

[ApiController]
[Route("api/v1/transactions")]
[Authorize]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionsController(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyHistory()
    {
        string? userIdClaim = User.FindFirst("sub")?.Value;
        if (!Guid.TryParse(userIdClaim, out Guid playerId)) return Unauthorized();

        var transactions = await _transactionRepository.GetByPlayerIdAsync(playerId);
        
        return Ok(transactions);
    }
}
