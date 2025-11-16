using PersonalFinanceAccounting.Application;
using PersonalFinanceAccounting.Application.Models.Exceptions;

namespace PersonalFinanceAccounting;

public class Menu
{
    private readonly ReportService _reportService;

    public Menu(ReportService reportService)
    {
        _reportService = reportService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("=== Учёт личных финансов ===");

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1. Показать транзакции по группам");
            Console.WriteLine("2. Показать топ-3 расходов");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите пункт: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await ShowTransactionGroupsAsync(cancellationToken);
                    break;

                case "2":
                    await ShowTop3ExpensesAsync(cancellationToken);
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Неверный ввод. Попробуйте снова.");
                    break;
            }
        }
    }

    private DateOnly ReadDate()
    {
        Console.Write("Введите год: ");
        int year = int.Parse(Console.ReadLine()!);

        Console.Write("Введите месяц (1–12): ");
        int month = int.Parse(Console.ReadLine()!);

        // День можно ставить 1, т.к. нас интересуют только год+месяц
        return new DateOnly(year, month, 1);
    }

    private async Task ShowTransactionGroupsAsync(CancellationToken cancellationToken)
    {
        try
        {
            var date = ReadDate();

            var groups = await _reportService.GetTransactionsGroupedByTypeAsync(date, cancellationToken);

            ReportPrinter.PrintTransactionGroups(groups, date.Year, date.Month);
        }
        catch (TransactionListIsEmptyException)
        {
            Console.WriteLine("Транзакции за указанный месяц отсутствуют.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private async Task ShowTop3ExpensesAsync(CancellationToken cancellationToken)
    {
        try
        {
            var date = ReadDate();

            var data = await _reportService.GetTop3ExpensesPerWalletAsync(date, cancellationToken);

            ReportPrinter.PrintTop3Expenses(data, date.Year, date.Month);
        }
        catch (TransactionListIsEmptyException)
        {
            Console.WriteLine("Расходы за указанный месяц отсутствуют.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}