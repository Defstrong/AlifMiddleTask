using BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace Presentation;

[Route("api/[controller]")]
public class WalletController : ControllerBase
{
    private readonly IWalletService _walletService;

    public WalletController(IWalletService walletService)
    {
        ArgumentNullException.ThrowIfNull(walletService);

        _walletService = walletService;
    }

    [HttpPost("check/{id}")]
    public async Task<IActionResult> CheckAsync(string id, CancellationToken cancellationToken = default)
    {
        bool walletExists = await _walletService.CheckAsync(id, cancellationToken);

        if(walletExists)
            return Ok("Such a wallet exists");

        return NotFound("No such wallet exists");
    }

    [HttpPost("toup")]
    public async Task<IActionResult> ToUpAsync([FromBody] ToUpDto toUpDto, CancellationToken cancellationToken = default)
    {
        if(!ModelState.IsValid)
            return Ok(ModelState.Values
                .SelectMany(model => model.Errors.Select(error => error.ErrorMessage)));

        bool result = await _walletService.TopUpAsync(toUpDto, cancellationToken);

        if(result)
            return Ok("Wallet successfully topped up.");

        return BadRequest("Wallet either not found or full.");
    }

    [HttpPost("sum-of-transactions/{id}")]
    public async Task<IActionResult> TransactionsSummnaryAsync(string id, CancellationToken cancellationToken = default)
    {
        TransactionSummaryDto transactionSummaryDto = await _walletService.TransactionsSummnaryAsync(id, cancellationToken);
        
        return Ok(transactionSummaryDto);
    }

    [HttpPost("balance/{id}")]
    public async Task<IActionResult> GetBalanceAsync(string id, CancellationToken cancellationToken = default)
    {
        decimal result = await _walletService.GetBalanceAsync(id, cancellationToken);
        
        return Ok(result);
    }
}