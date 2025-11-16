using PersonalFinanceAccounting.Infrastructure.Models;

namespace PersonalFinanceAccounting.Application.Models;

public class WalletTopExpensesResult
{
    public Wallet Wallet { get; set; } = null!;
    public IReadOnlyList<Transaction> TopExpenses { get; set; } = [];
}