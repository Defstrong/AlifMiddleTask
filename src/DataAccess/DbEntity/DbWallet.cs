namespace DataAccess;

public record DbWallet
{
    private readonly string? _id;

    public string Id
    {
        get => _id ?? string.Empty;
        init => _id = value is { Length: > 0 }
            ? value : Guid.NewGuid().ToString();
    }

    public decimal Balance { get; set; }

    public WalletStatus Status { get; init; }

    public virtual DbClient? Client { get; init; }

    public virtual ICollection<DbWalletTransaction>? WalletTrancations { get; init; }
}