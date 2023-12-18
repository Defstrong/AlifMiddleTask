using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddWalletTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "wallets_transactions",
                columns: table => new
                {
                    id = table.Column<string>(type: "VARCHAR", nullable: false),
                    time_of_transacition = table.Column<DateTime>(type: "DATE", nullable: false),
                    quantity = table.Column<decimal>(type: "DECIMAL", nullable: false),
                    status = table.Column<int>(type: "INTEGER", nullable: false),
                    WalletId = table.Column<string>(type: "VARCHAR", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wallets_transactions", x => x.id);
                    table.ForeignKey(
                        name: "FK_wallets_transactions_wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "wallets",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "wallets",
                keyColumn: "id",
                keyValue: "1ef8c2da-4ac1-461b-a205-603fa8fd88cf",
                column: "balance",
                value: 4047m);

            migrationBuilder.UpdateData(
                table: "wallets",
                keyColumn: "id",
                keyValue: "524c1940-3df9-4332-8041-d560e285aff2",
                columns: new[] { "balance", "status" },
                values: new object[] { 4047m, 0 });

            migrationBuilder.InsertData(
                table: "wallets_transactions",
                columns: new[] { "id", "quantity", "status", "time_of_transacition", "WalletId" },
                values: new object[,]
                {
                    { "0d12654d-dd56-47c6-8001-35c781d59d55", 2024m, 1, new DateTime(2023, 12, 18, 21, 43, 59, 82, DateTimeKind.Local).AddTicks(1379), "524c1940-3df9-4332-8041-d560e285aff2" },
                    { "443112df-5112-493b-b863-6029245de36e", 2023m, 1, new DateTime(2023, 11, 18, 21, 43, 59, 82, DateTimeKind.Local).AddTicks(1348), "524c1940-3df9-4332-8041-d560e285aff2" },
                    { "9be32e58-8e3c-40d7-be0a-bb155d20e570", 120000m, 0, new DateTime(2023, 12, 18, 21, 43, 59, 82, DateTimeKind.Local).AddTicks(1391), "1ef8c2da-4ac1-461b-a205-603fa8fd88cf" },
                    { "a048a76f-c935-41d6-a76a-802b463bd5ce", 12000m, 0, new DateTime(2023, 12, 18, 21, 43, 59, 82, DateTimeKind.Local).AddTicks(1382), "524c1940-3df9-4332-8041-d560e285aff2" },
                    { "b15e419f-9402-4714-8b13-584387db7b70", 2023m, 1, new DateTime(2023, 11, 18, 21, 43, 59, 82, DateTimeKind.Local).AddTicks(1385), "1ef8c2da-4ac1-461b-a205-603fa8fd88cf" },
                    { "ff5fbe30-a4aa-4e85-8919-1c76a6ded6ce", 2024m, 1, new DateTime(2023, 12, 18, 21, 43, 59, 82, DateTimeKind.Local).AddTicks(1388), "1ef8c2da-4ac1-461b-a205-603fa8fd88cf" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_wallets_transactions_WalletId",
                table: "wallets_transactions",
                column: "WalletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wallets_transactions");

            migrationBuilder.UpdateData(
                table: "wallets",
                keyColumn: "id",
                keyValue: "1ef8c2da-4ac1-461b-a205-603fa8fd88cf",
                column: "balance",
                value: 15000m);

            migrationBuilder.UpdateData(
                table: "wallets",
                keyColumn: "id",
                keyValue: "524c1940-3df9-4332-8041-d560e285aff2",
                columns: new[] { "balance", "status" },
                values: new object[] { 1500m, 2 });
        }
    }
}
