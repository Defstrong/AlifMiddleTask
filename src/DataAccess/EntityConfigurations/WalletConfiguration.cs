using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess;

public sealed class WalletConfiguration : IEntityTypeConfiguration<DbWallet>
{
    public void Configure(EntityTypeBuilder<DbWallet> builder)
    {
        builder
            .ToTable("wallets")
            .HasKey(wallet => wallet.Id);
        
        builder
            .Property(wallet => wallet.Id)
            .HasColumnName("id")
            .HasColumnType("VARCHAR")
            .IsRequired();
        
        builder
            .Property(wallet => wallet.Balance)
            .HasColumnName("balance")
            .HasColumnType("DECIMAL")
            .IsRequired();
            
        builder
            .Property(wallet => wallet.Status)
            .HasColumnName("status")
            .HasColumnType("INTEGER")
            .IsRequired();
        
        builder
            .HasMany(wallet => wallet.WalletTrancations)
            .WithOne(walletTransaction => walletTransaction.Wallet)
            .HasForeignKey(walletTransactoin => walletTransactoin.WalletId)
            .IsRequired(false);
        
        builder
            .HasData(
                new DbWallet()
                {
                    Id = "524c1940-3df9-4332-8041-d560e285aff2",
                    Balance = 2023 + 2024,
                    Status = WalletStatus.Unidentified
                },
                new DbWallet
                {
                    Id = "1ef8c2da-4ac1-461b-a205-603fa8fd88cf",
                    Balance = 2023 + 2024,
                    Status = WalletStatus.Identified
                }
            );
    }
}