using DataAccess;
using System.Runtime.CompilerServices;

namespace BusinessLogic;

public sealed class WalletService : IWalletService
{
    private readonly IWalletRepository _walletRepository;

    public WalletService(IWalletRepository walletRepository)
    {
        ArgumentNullException.ThrowIfNull(walletRepository);

        _walletRepository = walletRepository;
    }

    public Task<bool> CreateAsync(WalletDto model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        return _walletRepository.CreateAsync(model.DtoToWallet(), cancellationToken);
    }

    public Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(id);

        return _walletRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<WalletDto?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(id);

        DbWallet? dbwallet = await _walletRepository.GetAsync(id, cancellationToken);
        return dbwallet?.WalletToDto();
    }

    public async IAsyncEnumerable<WalletDto> GetAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (DbWallet walletDto in _walletRepository.GetAsync(cancellationToken))
            yield return walletDto.WalletToDto();
    }

    public Task<bool> UpdateAsync(WalletDto model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        DbWallet dbwallet = model.DtoToWallet();
        return _walletRepository.UpdateAsync(dbwallet, cancellationToken);
    }

    public Task<bool> CheckAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(id);

        return _walletRepository.CheckAsync(id, cancellationToken);
    }

    public async Task<bool> TopUpAsync(ToUpDto toUpDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(toUpDto);

        DbWallet? wallet = await _walletRepository.GetAsync(toUpDto.WalletId, cancellationToken);

        if(wallet is null) return false;

        bool result = wallet.ValidBalance(toUpDto.Quantity);

        wallet.Balance += result ? toUpDto.Quantity : 0;

        wallet.WalletTrancations?.Add(
            new()
            {
                Id = Guid.NewGuid().ToString(),
                TimeOfTransacition = DateTime.Now,
                Quantity = toUpDto.Quantity,
                Status = result ? TransactionStatus.Successfully : TransactionStatus.NotSuccessful
            }
        );

        await _walletRepository.UpdateAsync(wallet, cancellationToken);
        return result;
    }

    public async Task<TransactionSummaryDto> TransactionsSummnaryAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(id);

        TransactionSummary transactionSummary = await _walletRepository.TransactionsSummnaryAsync(id, cancellationToken);

        return transactionSummary.TransactionSummaryToDto();
    }

    public Task<decimal> GetBalanceAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(id);

        return _walletRepository.GetBalanceAsync(id, cancellationToken);
    }
}
