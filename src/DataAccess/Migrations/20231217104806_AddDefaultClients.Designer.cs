﻿// <auto-generated />
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(AlifDbContext))]
    [Migration("20231217104806_AddDefaultClients")]
    partial class AddDefaultClients
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataAccess.DbClient", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("VARCHAR")
                        .HasColumnName("id");

                    b.Property<string>("Digest")
                        .IsRequired()
                        .HasColumnType("VARCHAR")
                        .HasColumnName("digest");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR")
                        .HasColumnName("name");

                    b.Property<string>("WalletId")
                        .IsRequired()
                        .HasColumnType("VARCHAR")
                        .HasColumnName("wallet_id");

                    b.HasKey("Id");

                    b.HasIndex("WalletId")
                        .IsUnique();

                    b.ToTable("clients", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "4f2e5dd4-f012-4f44-b0ee-9cc924610476",
                            Digest = "",
                            Name = "First Client",
                            WalletId = "524c1940-3df9-4332-8041-d560e285aff2"
                        },
                        new
                        {
                            Id = "de9cddb0-5673-4616-8019-04de8673af1b",
                            Digest = "",
                            Name = "Second Client",
                            WalletId = "1ef8c2da-4ac1-461b-a205-603fa8fd88cf"
                        });
                });

            modelBuilder.Entity("DataAccess.DbWallet", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("VARCHAR")
                        .HasColumnName("id");

                    b.Property<decimal>("Balance")
                        .HasColumnType("DECIMAL")
                        .HasColumnName("balance");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.ToTable("wallets", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "524c1940-3df9-4332-8041-d560e285aff2",
                            Balance = 1500m,
                            Status = 2
                        },
                        new
                        {
                            Id = "1ef8c2da-4ac1-461b-a205-603fa8fd88cf",
                            Balance = 15000m,
                            Status = 1
                        });
                });

            modelBuilder.Entity("DataAccess.DbClient", b =>
                {
                    b.HasOne("DataAccess.DbWallet", "Wallet")
                        .WithOne("Client")
                        .HasForeignKey("DataAccess.DbClient", "WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("DataAccess.DbWallet", b =>
                {
                    b.Navigation("Client");
                });
#pragma warning restore 612, 618
        }
    }
}
