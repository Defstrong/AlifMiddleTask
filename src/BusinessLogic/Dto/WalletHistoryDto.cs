// namespace BusinessLogic;

// public record WalletHistoryDto
// {
//     private readonly string? _id;

//     public string Id
//     {
//         get => _id ?? string.Empty;
//         init => _id = value is { Length: > 0 }
//             ? value : Guid.NewGuid().ToString();
//     }

//     public string WalletId { get; init; } = string.Empty;

//     public ICollection<WalletTransactionDto>? Transactions { get; init; }

//     public WalletDto? Wallet { get; init; }
// }