using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public sealed class WalletRepository : BaseRepository<DbWallet>, IWalletRepository
{
    private readonly AlifDbContext _db;
    public WalletRepository(AlifDbContext db) : base(db) => _db = db;

    public Task<bool> CheckAsync(string id, CancellationToken cancellationToken = default)
        => _db.Wallets.AnyAsync(wallet => wallet.Id == id, cancellationToken);

    public async Task<TransactionSummary> TransactionsSummnaryAsync(string id, CancellationToken cancellationToken = default)
    {
        decimal totalAmount = await _db.WalletsTransactions
            .Where(walletTransaction => walletTransaction.WalletId == id
                && walletTransaction.Status == TransactionStatus.Successfully
                && walletTransaction.TimeOfTransacition.Month == DateTime.Now.Month)
            .SumAsync(walletTransaction => walletTransaction.Quantity, cancellationToken);

        int totalOperations = await _db.WalletsTransactions
            .CountAsync(walletTransaction => walletTransaction.WalletId == id
                && walletTransaction.Status == TransactionStatus.Successfully
                && walletTransaction.TimeOfTransacition.Month == DateTime.Now.Month, cancellationToken);

        return new () { TotalAmount = totalAmount, TotalOperations = totalOperations };
    }

    public async Task<decimal> GetBalanceAsync(string id, CancellationToken cancellationToken = default)
    {
        if(await CheckAsync(id, cancellationToken))
            return _db.Wallets.First(wallet => wallet.Id == id).Balance;

        return 0;
    }
}
