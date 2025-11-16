namespace PersonalFinanceAccounting.Application.Models.Exceptions;

public class TransactionListIsEmptyException : Exception
{
    public TransactionListIsEmptyException()
        : base("Список транзакций пуст.") { }

    public TransactionListIsEmptyException(string message)
        : base(message) { }

    public TransactionListIsEmptyException(string message, Exception innerException)
        : base(message, innerException) { }
}