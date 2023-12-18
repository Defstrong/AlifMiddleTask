namespace BusinessLogic;

public interface IWalletService : IBaseService<WalletDto> 
{ 
    Task<bool> CheckAsync(string id, CancellationToken cancellationToken = default);
    Task<bool> TopUpAsync(ToUpDto toUpDto, CancellationToken cancellationToken = default);
    Task<decimal> SumOfTransactionsAsync(string id, CancellationToken cancellationToken = default);
    Task<decimal> GetBalanceAsync(string id, CancellationToken cancellationToken = default);
}