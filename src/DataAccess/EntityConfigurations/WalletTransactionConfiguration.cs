using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess;

public sealed class WalletTransactionConfiguration : IEntityTypeConfiguration<DbWalletTransaction>
{
    public void Configure(EntityTypeBuilder<DbWalletTransaction> builder)
    {
        builder
            .ToTable("wallets_transactions")
            .HasKey(walletTransaction => walletTransaction.Id);
        
        builder
            .Property(walletTransaction => walletTransaction.Id)
            .HasColumnName("id")
            .HasColumnType("VARCHAR")
            .IsRequired();
        
        builder
            .Property(walletTransaction => walletTransaction.TimeOfTransacition)
            .HasColumnName("time_of_transacition")
            .HasColumnType("DATE")
            .IsRequired();
        
        builder
            .Property(walletTransaction => walletTransaction.Quantity)
            .HasColumnName("quantity")
            .HasColumnType("DECIMAL")
            .IsRequired();
        
        builder
            .Property(walletTransaction => walletTransaction.Status)
            .HasColumnName("status")
            .HasColumnType("INTEGER")
            .IsRequired();
        
        builder
            .HasData(
                new DbWalletTransaction
                {
                    Id = "443112df-5112-493b-b863-6029245de36e",
                    TimeOfTransacition = DateTime.Now.AddMonths(-1),
                    Quantity = 2023,
                    Status = TransactionStatus.Successfully,
                    WalletId = "524c1940-3df9-4332-8041-d560e285aff2"
                },
                new DbWalletTransaction
                {
                    Id = "0d12654d-dd56-47c6-8001-35c781d59d55",
                    TimeOfTransacition = DateTime.Now,
                    Quantity = 2024,
                    Status = TransactionStatus.Successfully,
                    WalletId = "524c1940-3df9-4332-8041-d560e285aff2"
                },
                new DbWalletTransaction
                {
                    Id = "a048a76f-c935-41d6-a76a-802b463bd5ce",
                    TimeOfTransacition = DateTime.Now,
                    Quantity = 12000,
                    Status = TransactionStatus.NotSuccessful,
                    WalletId = "524c1940-3df9-4332-8041-d560e285aff2"
                },
                new DbWalletTransaction
                {
                    Id = "b15e419f-9402-4714-8b13-584387db7b70",
                    TimeOfTransacition = DateTime.Now.AddMonths(-1),
                    Quantity = 2023,
                    Status = TransactionStatus.Successfully,
                    WalletId = "1ef8c2da-4ac1-461b-a205-603fa8fd88cf"
                },
                new DbWalletTransaction
                {
                    Id = "ff5fbe30-a4aa-4e85-8919-1c76a6ded6ce",
                    TimeOfTransacition = DateTime.Now,
                    Quantity = 2024,
                    Status = TransactionStatus.Successfully,
                    WalletId = "1ef8c2da-4ac1-461b-a205-603fa8fd88cf"
                },
                new DbWalletTransaction
                {
                    Id = "9be32e58-8e3c-40d7-be0a-bb155d20e570",
                    TimeOfTransacition = DateTime.Now,
                    Quantity = 120000,
                    Status = TransactionStatus.NotSuccessful,
                    WalletId = "1ef8c2da-4ac1-461b-a205-603fa8fd88cf"
                }
            );
    }
}