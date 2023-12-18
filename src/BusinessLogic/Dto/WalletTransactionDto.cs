using DataAccess;

namespace BusinessLogic;

public sealed record WalletTransactionDto
{
    private readonly string? _id;

    public string Id
    {
        get => _id ?? string.Empty;
        init => _id = value is { Length: > 0 }
            ? value : Guid.NewGuid().ToString();
    }

    public DateTime TimeOfTransacition { get; init; }

    public decimal Quantity { get; init; }

    public TransactionStatus Status { get; init; }

    public string WalletId { get; init; } = string.Empty;

    public WalletDto? Wallet { get; init; }
}