using PersonalFinanceAccounting.Infrastructure.Models.Enums;

namespace PersonalFinanceAccounting.Infrastructure.Models;

public class Transaction
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public string Description { get; set; } = string.Empty;

    public Guid WalletId { get; set; }
    public Wallet Wallet { get; set; } = null!;
}
