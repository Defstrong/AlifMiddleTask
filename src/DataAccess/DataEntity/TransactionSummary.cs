namespace DataAccess;

public record TransactionSummary
{
    public int TotalOperations { get; set; }
    public decimal TotalAmount { get; set; }
}