using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMarketSimulationsRest.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersTransactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    StockId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockPriceAtMoment = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    IsBuy = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    TransactionValue = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTransactions", x => x.TransactionId);
                });

            migrationBuilder.CreateTable(
                name: "UsersWallets",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsWithdraw = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    TransactionValue = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersWallets", x => x.TransactionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersTransactions_UserId",
                table: "UsersTransactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersWallets_UserId",
                table: "UsersWallets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersTransactions");

            migrationBuilder.DropTable(
                name: "UsersWallets");
        }
    }
}
