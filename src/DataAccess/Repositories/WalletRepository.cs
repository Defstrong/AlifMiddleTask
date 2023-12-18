using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public sealed class WalletRepository : BaseRepository<DbWallet>, IWalletRepository
{
    private readonly AlifDbContext _db;
    public WalletRepository(AlifDbContext db) : base(db) => _db = db;

    public Task<bool> CheckAsync(string id, CancellationToken cancellationToken = default)
        => _db.Wallets.AnyAsync(wallet => wallet.Id == id);
    
    public Task<decimal> SumOfTransactionsAsync(string id, CancellationToken cancellationToken = default)
        => _db.WalletsTransactions
            .Where(walletTransaction => walletTransaction.WalletId == id 
                && walletTransaction.Status == TransactionStatus.Successfully
                && walletTransaction.TimeOfTransacition.Month == DateTime.Now.Month)
            .SumAsync(walletTransaction => walletTransaction.Quantity);

    public async Task<decimal> GetBalanceAsync(string id, CancellationToken cancellationToken = default)
    {
        if(await CheckAsync(id, cancellationToken))
            return _db.Wallets.First(wallet => wallet.Id == id).Balance;

        return 0;
    }
}