using PersonalFinanceAccounting.Application.Models;


namespace PersonalFinanceAccounting;

public static class ReportPrinter
{
    public static void PrintTransactionGroups(IReadOnlyList<TransactionGroupResult> groups, int year, int month)
    {
        Console.WriteLine();
        Console.WriteLine($"=== Транзакции за {month:D2}.{year} сгруппированы по типам ===");

        foreach (var group in groups)
        {
            Console.WriteLine();
            Console.WriteLine($"Тип: {group.Type} | Общая сумма: {group.Total:F2}");

            foreach (var t in group.Transactions)
            {
                Console.WriteLine(
                    $"{t.Date:dd.MM.yyyy} | {t.Amount,10:F2} {t.Wallet.Currency} | {t.Wallet.Name} | {t.Description}");
            }
        }
    }

    public static void PrintTop3Expenses(IReadOnlyList<WalletTopExpensesResult> data, int year, int month)
    {
        Console.WriteLine();
        Console.WriteLine($"=== Топ-3 расходов по каждому кошельку за {month:D2}.{year} ===");

        foreach (var item in data)
        {
            Console.WriteLine();
            Console.WriteLine($"Кошелек: {item.Wallet.Name} ({item.Wallet.Currency})");

            if (item.TopExpenses.Count == 0)
            {
                Console.WriteLine("  Нет расходов");
                continue;
            }

            foreach (var t in item.TopExpenses)
            {
                Console.WriteLine(
                    $"{t.Date:dd.MM.yyyy} | {t.Amount,10:F2} {item.Wallet.Currency} | {t.Description}");
            }
        }
    }
}