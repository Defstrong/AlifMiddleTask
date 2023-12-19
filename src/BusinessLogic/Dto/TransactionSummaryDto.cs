namespace BusinessLogic;

public sealed record TransactionSummaryDto
{
    public int TotalOperations { get; set; }
    public decimal TotalAmount { get; set; }
}