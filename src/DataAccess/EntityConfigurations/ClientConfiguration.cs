using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess;

public sealed class ClientConfiguration : IEntityTypeConfiguration<DbClient>
{
    public void Configure(EntityTypeBuilder<DbClient> builder)
    {
        builder
            .ToTable("clients")
            .HasKey(client => client.Id);
        
        builder
            .Property(client => client.Id)
            .HasColumnName("id")
            .HasColumnType("VARCHAR")
            .IsRequired();
        
        builder
            .Property(client => client.Name)
            .HasColumnName("name")
            .HasColumnType("VARCHAR")
            .IsRequired();
        
        builder
            .Property(client => client.Digest)
            .HasColumnName("digest")
            .HasColumnType("VARCHAR")
            .IsRequired();

        builder
            .Property(client => client.WalletId)
            .HasColumnName("wallet_id")
            .HasColumnType("VARCHAR")
            .IsRequired();
        
        builder
            .HasOne(client => client.Wallet)
            .WithOne(wallet => wallet.Client)
            .HasForeignKey<DbClient>(cleint => cleint.WalletId)
            .IsRequired();
        
        builder
            .HasData(
                new DbClient
                {
                    Id = "4f2e5dd4-f012-4f44-b0ee-9cc924610476",
                    Name = "First Client",
                    WalletId = "524c1940-3df9-4332-8041-d560e285aff2",
                },
                new DbClient
                {
                    Id = "de9cddb0-5673-4616-8019-04de8673af1b",
                    Name = "Second Client",
                    WalletId = "1ef8c2da-4ac1-461b-a205-603fa8fd88cf",
                }
            );
    }
}