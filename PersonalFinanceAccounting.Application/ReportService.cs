using Microsoft.EntityFrameworkCore;
using PersonalFinanceAccounting.Application.Models;
using PersonalFinanceAccounting.Application.Models.Exceptions;
using PersonalFinanceAccounting.Infrastructure.Data;
using PersonalFinanceAccounting.Infrastructure.Models.Enums;

namespace PersonalFinanceAccounting.Application;

public class ReportService : IReportService
{
    private readonly PfaDbContext _context;

    public ReportService(PfaDbContext context)
    {
        _context = context;
    }
    
    public async Task<IReadOnlyList<TransactionGroupResult>> GetTransactionsGroupedByTypeAsync(DateOnly date, CancellationToken cancellationToken)
    {
        var transactions = await _context.Transactions
            .Include(t => t.Wallet)
            .Where(t => t.Date.Year == date.Year && t.Date.Month == date.Month)
            .ToListAsync(cancellationToken);

        if (transactions.Count == 0)
        {
            throw new TransactionListIsEmptyException();
        }

        var grouped = transactions
            .GroupBy(t => t.Type)
            .Select(g => new TransactionGroupResult
            {
                Type = g.Key,
                Total = g.Sum(x => x.Amount),
                Transactions = g
                    .OrderBy(x => x.Date)
                    .ThenBy(x => x.Id)
                    .ToList()
            })
            .OrderByDescending(x => x.Total)
            .ToList();

        return grouped;
    }
    
    public async Task<IReadOnlyList<WalletTopExpensesResult>> GetTop3ExpensesPerWalletAsync(DateOnly date, CancellationToken cancellationToken)
    {
        var expenses = await _context.Transactions
            .Include(t => t.Wallet)
            .Where(t => t.Type == TransactionType.Expense
                        && t.Date.Year == date.Year
                        && t.Date.Month == date.Month)
            .ToListAsync(cancellationToken);

        if (expenses.Count == 0)
        {
            throw new TransactionListIsEmptyException("Расходы за указанный период отсутствуют.");
        }

        var grouped = expenses
            .GroupBy(t => t.Wallet)
            .Select(g => new WalletTopExpensesResult
            {
                Wallet = g.Key,
                TopExpenses = g
                    .OrderByDescending(t => t.Amount)
                    .Take(3)
                    .ToList()
            })
            .OrderBy(r => r.Wallet.Name)
            .ToList();

        return grouped;
    }
}
