using DataAccess;

namespace BusinessLogic;

/// <summary>
///     Represents a class for convert WalletDto to DbWallet and vice versa.
/// </summary>
public static class WalletExtensions
{
    private static readonly decimal _maxUnidentifierUserBalance = 10_000;
    private static readonly decimal _maxIdentifierUserBalance = 100_000;
    public static WalletDto WalletToDto(this DbWallet dbWallet)
        => new()
        {
            Id = dbWallet.Id,
            Balance = dbWallet.Balance,
            Status = dbWallet.Status
        };

    public static DbWallet DtoToWallet(this WalletDto walletDto)
        => new()
        {
            Id = walletDto.Id,
            Balance = walletDto.Balance,
            Status = walletDto.Status
        };
    
    public static bool ValidBalance(this DbWallet wallet, decimal quantity)
    {
        bool result = false;
        DbWalletTransaction transaction = 
            new ()
            {
                Id = Guid.NewGuid().ToString(),
                TimeOfTransacition = DateTime.Now,
                Quantity = quantity,
            };

        if(wallet is null) return false;

        if(wallet.Status == WalletStatus.Unidentified)
        {
            if((wallet.Balance + quantity) <= 10_000)
            {
                wallet.Balance += quantity;
                transaction.Status = TransactionStatus.Successfully;
                result = true;
            }
        }
        else
        {
            if((wallet.Balance + quantity) <= 100_000)
            {
                wallet.Balance += quantity;
                transaction.Status = TransactionStatus.Successfully;
                result = true;
            }
        }

        wallet.WalletTrancations?.Add(transaction);
        return result;
    }
}