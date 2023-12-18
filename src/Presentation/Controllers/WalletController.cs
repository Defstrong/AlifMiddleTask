using BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace Presentation;

[Route("wallet")]
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
            return Ok(walletExists);

        return NotFound(walletExists);
    }

    [HttpPost("toup")]
    public async Task<IActionResult> ToUpAsync([FromBody]ToUpDto toUpDto, CancellationToken cancellationToken = default)
    {
        if(toUpDto.Quantity <= 0)
            return BadRequest("The amount for replenishment must be greater than 0.");

        bool result = await _walletService.TopUpAsync(toUpDto, cancellationToken);

        if(result)
            return Ok("Wallet successfully topped up.");

        return BadRequest("Wallet either not found or full.");
    }

    [HttpPost("sumoftransactions/{id}")]
    public async Task<IActionResult> SumOfTransactionsAsync(string id, CancellationToken cancellationToken = default)
    {
        decimal result = await _walletService.SumOfTransactionsAsync(id, cancellationToken);
        
        return Ok(result);
    }

    [HttpPost("balance/{id}")]
    public async Task<IActionResult> GetBalanceAsync(string id, CancellationToken cancellationToken = default)
    {
        decimal result = await _walletService.GetBalanceAsync(id, cancellationToken);
        
        return Ok(result);
    }
}