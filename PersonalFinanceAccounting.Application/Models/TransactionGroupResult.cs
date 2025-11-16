using PersonalFinanceAccounting.Infrastructure.Models;
using PersonalFinanceAccounting.Infrastructure.Models.Enums;

namespace PersonalFinanceAccounting.Application.Models;

public class TransactionGroupResult
{
    public TransactionType Type { get; set; }
    
    public decimal Total { get; set; }
    
    public IReadOnlyList<Transaction> Transactions { get; set; } = [];
}