using DataAccess;

namespace BusinessLogic;

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
        => wallet switch
        {
            { Balance: decimal balance , Status: WalletStatus.Unidentified }
                when (balance + quantity) <= _maxUnidentifierUserBalance => true,
            { Balance: decimal balance , Status: WalletStatus.Identified }
                when (balance + quantity) <= _maxIdentifierUserBalance => true,
            _ => false
        };
}
