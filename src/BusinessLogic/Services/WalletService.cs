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

    public Task<bool> CreateAsync(WalletDto model, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        return _walletRepository.CreateAsync(model.DtoToWallet(), token);
    }

    public Task<bool> DeleteAsync(string id, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(id);

        return _walletRepository.DeleteAsync(id, token);
    }

    public async Task<WalletDto?> GetAsync(string id, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(id);

        DbWallet? dbwallet = await _walletRepository.GetAsync(id, token);
        return dbwallet?.WalletToDto();
    }

    public async IAsyncEnumerable<WalletDto> GetAsync([EnumeratorCancellation] CancellationToken token = default)
    {
        await foreach (DbWallet walletDto in _walletRepository.GetAsync(token))
            yield return walletDto.WalletToDto();
    }

    public Task<bool> UpdateAsync(WalletDto model, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        DbWallet dbwallet = model.DtoToWallet();
        return _walletRepository.UpdateAsync(dbwallet, token);
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

        bool result = ValidBalance(wallet, toUpDto.Quantity);

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

    public Task<decimal> SumOfTransactionsAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(id);

        return _walletRepository.SumOfTransactionsAsync(id, cancellationToken);
    }

    public Task<decimal> GetBalanceAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(id);

        return _walletRepository.GetBalanceAsync(id, cancellationToken);
    }

    public bool ValidBalance(DbWallet wallet, decimal quantity)
        => wallet switch
        {
            { Balance: decimal balance , Status: WalletStatus.Unidentified } when (balance + quantity) <= 10_000 => true,
            { Balance: decimal balance , Status: WalletStatus.Identified } when (balance + quantity) <= 100_000 => true,
            _ => false
        };
}