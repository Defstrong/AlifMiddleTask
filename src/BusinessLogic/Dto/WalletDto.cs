using DataAccess;

namespace BusinessLogic;

public sealed record WalletDto
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

    public ClientDto? Client { get; init; }

    public ICollection<WalletTransactionDto>? WalletTransactions { get; init; }
}