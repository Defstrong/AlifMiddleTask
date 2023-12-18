namespace BusinessLogic;

public sealed record ToUpDto
{
    public string WalletId { get; init; } = string.Empty;
    public decimal Quantity { get; init; }
}