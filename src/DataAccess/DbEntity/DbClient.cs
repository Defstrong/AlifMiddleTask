namespace DataAccess;

public record DbClient
{
    private readonly string? _id;
    
    public string Id
    {
        get => _id ?? string.Empty;
        init => _id = value is { Length: > 0}
            ? value : Guid.NewGuid().ToString();
    }

    public string Name { get; init; } = string.Empty;

    public string WalletId { get; init; } = string.Empty;

    public virtual DbWallet? Wallet { get; init; }
}