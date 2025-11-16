using PersonalFinanceAccounting.Infrastructure.Models.Enums;

namespace PersonalFinanceAccounting.Infrastructure.Models;

public class Wallet
{
    private readonly List<Transaction> _transactions = new();
    
    
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public Currency Currency { get; set; } = Currency.RUB;
    
    public decimal InitialBalance { get; set; }

    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

    public decimal CurrentBalance =>
        InitialBalance
        + _transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount)
        - _transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);

    public decimal GetMonthlyIncome(int year, int month) =>
        _transactions
            .Where(t => t.Type == TransactionType.Income
                        && t.Date.Year == year
                        && t.Date.Month == month)
            .Sum(t => t.Amount);

    public decimal GetMonthlyExpense(int year, int month) =>
        _transactions
            .Where(t => t.Type == TransactionType.Expense
                        && t.Date.Year == year
                        && t.Date.Month == month)
            .Sum(t => t.Amount);

    public void AddTransaction(Transaction transaction)
    {
        if (transaction.Type == TransactionType.Expense &&
            CurrentBalance - transaction.Amount < 0)
        {
            throw new InvalidOperationException(
                "Нельзя создать расход: сумма превышает текущий баланс кошелька.");
        }

        _transactions.Add(transaction);
    }
}