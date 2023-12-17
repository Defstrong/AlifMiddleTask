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

    public decimal Balance { get; init; }

    public WalletStatus Status { get; init; }

    public DbClient? Client { get; init; }
}