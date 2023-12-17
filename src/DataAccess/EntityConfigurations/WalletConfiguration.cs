using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess;

public sealed class WalletConfiguration : IEntityTypeConfiguration<DbWallet>
{
    public void Configure(EntityTypeBuilder<DbWallet> builder)
    {
        builder
            .ToTable("wallets")
            .HasKey(client => client.Id);
        
        builder
            .Property(client => client.Id)
            .HasColumnName("id")
            .HasColumnType("VARCHAR")
            .IsRequired();
        
        builder
            .Property(client => client.Balance)
            .HasColumnName("balance")
            .HasColumnType("DECIMAL")
            .IsRequired();
            
        builder
            .Property(client => client.Status)
            .HasColumnName("status")
            .HasColumnType("INTEGER")
            .IsRequired();
        
        builder
            .HasData(
                new DbWallet()
                {
                    Id = "524c1940-3df9-4332-8041-d560e285aff2",
                    Balance = 1500,
                    Status = WalletStatus.Unidentified
                },
                new DbWallet
                {
                    Id = "1ef8c2da-4ac1-461b-a205-603fa8fd88cf",
                    Balance = 15000,
                    Status = WalletStatus.Identified
                }
            );
    }
}