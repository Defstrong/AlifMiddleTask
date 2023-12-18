namespace DataAccess;

public record DbWalletTransaction
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

    public TransactionStatus Status { get; set; }

    public string WalletId { get; init; } = string.Empty;

    public virtual DbWallet? Wallet { get; init; }
}