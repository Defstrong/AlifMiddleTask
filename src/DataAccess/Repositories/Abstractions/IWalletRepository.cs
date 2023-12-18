namespace DataAccess;

public interface IWalletRepository : IBaseRepository<DbWallet> 
{
    Task<bool> CheckAsync(string id, CancellationToken cancellationToken = default);
    Task<decimal> SumOfTransactionsAsync(string id, CancellationToken cancellationToken = default);
    Task<decimal> GetBalanceAsync(string id, CancellationToken cancellationToken = default);
}