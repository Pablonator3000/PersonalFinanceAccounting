using Microsoft.EntityFrameworkCore.Migrations;
using PersonalFinanceAccounting.Infrastructure.Models.Enums;

#nullable disable

namespace PersonalFinanceAccounting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var wallet1Id = Guid.NewGuid();
            var wallet2Id = Guid.NewGuid();

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Name", "Currency", "InitialBalance" },
                values: new object[,]
                {
                    { wallet1Id, "Основной кошелек", "RUB", 10000m },
                    { wallet2Id, "Долларовый счет", "USD", 500m }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[]
                {
                    "Id", "WalletId", "Date", "Amount", "Type", "Description"
                },
                values: new object[,]
                {
                    {
                        Guid.NewGuid(),
                        wallet1Id,
                        new DateTime(2025, 10, 1),
                        30000m,
                        nameof(TransactionType.Income),
                        "Зарплата"
                    },
                    {
                        Guid.NewGuid(),
                        wallet1Id,
                        new DateTime(2025, 10, 3),
                        1500m,
                        nameof(TransactionType.Expense),
                        "Продукты"
                    },
                    {
                        Guid.NewGuid(),
                        wallet1Id,
                        new DateTime(2025, 10, 5),
                        5000m,
                        nameof(TransactionType.Expense),
                        "Аренда"
                    },
                    {
                        Guid.NewGuid(),
                        wallet1Id,
                        new DateTime(2025, 10, 10),
                        2000m,
                        nameof(TransactionType.Expense),
                        "Покупка одежды"
                    },
                    {
                        Guid.NewGuid(),
                        wallet2Id,
                        new DateTime(2025, 10, 2),
                        200m,
                        nameof(TransactionType.Income),
                        "Перевод"
                    },
                    {
                        Guid.NewGuid(),
                        wallet2Id,
                        new DateTime(2025, 10, 4),
                        50m,
                        nameof(TransactionType.Expense),
                        "Подписка"
                    },
                    {
                        Guid.NewGuid(),
                        wallet2Id,
                        new DateTime(2025, 10, 6),
                        100m,
                        nameof(TransactionType.Expense),
                        "Онлайн-покупка"
                    }
                });
        }
        
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Transactions;");
            migrationBuilder.Sql("DELETE FROM Wallets;");
        }
    }
}
