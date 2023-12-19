using DataAccess;

namespace BusinessLogic;

public static class TransactionsSummnaryExtensions
{
    public static TransactionSummaryDto TransactionSummaryToDto(this TransactionSummary transactionSummary)
        => new()
        {
            TotalOperations = transactionSummary.TotalOperations,
            TotalAmount = transactionSummary.TotalAmount
        };

    public static TransactionSummary DtoToTransactionSummary(this TransactionSummaryDto transactionSummaryDto)
        => new()
        {
            TotalOperations = transactionSummaryDto.TotalOperations,
            TotalAmount = transactionSummaryDto.TotalAmount
        };
}