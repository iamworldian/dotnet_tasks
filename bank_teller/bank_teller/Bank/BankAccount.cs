using System.Transactions;
using bank_teller.Bank.Transactaion;
using bank_teller.SQL;
using bank_teller.User;

namespace bank_teller.Bank;

public class BankAccountClass : UserClass
{
    private string Password { get; set; }
    private int AdminOrNot { get; set; }
    private List<Transaction> Transactions;
    
    public BankAccountClass(string email,string name,string contact,string password,int adminOrNot) : base(email, name, contact)
    {
        Password = password;
        AdminOrNot = adminOrNot;
        Transactions = new List<Transaction>();
    }

    public bool SaveToDB()
    {
        return SQLExecutor.Exucute(
            $"INSERT INTO [User] (name,email,contact,password,admin) values ('{Name}','{Email}','{Contact}','{Password}',{AdminOrNot.ToString()})");
    }

    // public BankAccountClass(string email)
    // {
    //         
    // }

}