namespace bank_teller.Bank.Transactaion;

public class TransactionClass
{
    private decimal Amount { get; set; }
    private DateTime DateTime { get; set; }
    private string Notes { get; set; }

    public TransactionClass(decimal amount, DateTime dateTime, string notes)
    {
        Amount = amount;
        DateTime = dateTime;
        Notes = notes;
    }
}