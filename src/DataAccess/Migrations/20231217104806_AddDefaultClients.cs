using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultClients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "wallets",
                columns: new[] { "id", "balance", "status" },
                values: new object[,]
                {
                    { "1ef8c2da-4ac1-461b-a205-603fa8fd88cf", 15000m, 1 },
                    { "524c1940-3df9-4332-8041-d560e285aff2", 1500m, 2 }
                });

            migrationBuilder.InsertData(
                table: "clients",
                columns: new[] { "id", "digest", "name", "wallet_id" },
                values: new object[,]
                {
                    { "4f2e5dd4-f012-4f44-b0ee-9cc924610476", "", "First Client", "524c1940-3df9-4332-8041-d560e285aff2" },
                    { "de9cddb0-5673-4616-8019-04de8673af1b", "", "Second Client", "1ef8c2da-4ac1-461b-a205-603fa8fd88cf" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: "4f2e5dd4-f012-4f44-b0ee-9cc924610476");

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: "de9cddb0-5673-4616-8019-04de8673af1b");

            migrationBuilder.DeleteData(
                table: "wallets",
                keyColumn: "id",
                keyValue: "1ef8c2da-4ac1-461b-a205-603fa8fd88cf");

            migrationBuilder.DeleteData(
                table: "wallets",
                keyColumn: "id",
                keyValue: "524c1940-3df9-4332-8041-d560e285aff2");
        }
    }
}
