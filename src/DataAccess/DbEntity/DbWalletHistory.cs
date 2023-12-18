// namespace DataAccess;

// public record DbWalletHistory
// {
//     private readonly string? _id;

//     public string Id
//     {
//         get => _id ?? string.Empty;
//         init => _id = value is { Length: > 0 }
//             ? value : Guid.NewGuid().ToString();
//     }

//     public string WalletId { get; init; } = string.Empty;

//     public string WalletTransactionId { get; init; } = string.Empty;

//     public ICollection<DbWalletTransaction>? Transactions { get; init; }

//     public DbWallet? Wallet { get; init; }
// }