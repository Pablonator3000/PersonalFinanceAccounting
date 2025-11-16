using PersonalFinanceAccounting.Application.Models;

namespace PersonalFinanceAccounting.Application;

public interface IReportService
{
    Task<IReadOnlyList<TransactionGroupResult>> GetTransactionsGroupedByTypeAsync(DateOnly date, CancellationToken cancellationToken);
    Task<IReadOnlyList<WalletTopExpensesResult>> GetTop3ExpensesPerWalletAsync(DateOnly date, CancellationToken cancellationToken);
}