// See https://aka.ms/new-console-template for more information

using bank_teller.Bank;
using bank_teller.Logger;
using bank_teller.SQL;
using bank_teller.Helpers;
namespace bank_teller;

public class BankTeller
{
         
        private static void Main()
        {
                while (true)
                {
                        try
                        {
                                Console.WriteLine($"Welcome to BANK_TELLER\nLogin User:\n" +
                                                  $"1. Admin Login.\n" +
                                                  $"2. User Login.\n" +
                                                  $"0. To Exit");
                                string command = Console.ReadLine();
                                switch (command)
                                {
                                        case "1" :
                                                AdminLogin();
                                                break;
                                        case "2" :
                                                UserLogin();
                                                break;
                                        case "0" :
                                                ExitPrompt();
                                                break;
                                        default: break;
                                }

                                if (command == "0") break;
                        }
                        catch (Exception e)
                        {
                                Console.WriteLine(e);
                                throw;
                        }
                }
        }

        private static void AdminLogin()
        {
                while (true)
                {
                        Console.WriteLine($"Login Admin:\n" +
                                          $"Input Email: \n" +
                                          $"0 to exit");
                        string email = Console.ReadLine();
                        if (email == "0") break;
                        Console.WriteLine($"Input password:");
                        string password = Console.ReadLine();
                        
                        bool ret = SQLExecutor.Exucute($"select name,email from [User] where email='{email}' AND password='{password}'");

                        if (ret)
                        {
                                Console.WriteLine($"Admin Logged in\n\n");
                                LoggerExecuter.Execute(new UserLoginLogger($"{email} logged in as Admin", HelperClass.LoggerFilePath((int)LoggerFilePathEnum.UserLoginLogger)));
                                while (true)
                                {
                                        Console.WriteLine($"Welcome to Admin - {email}\n" +
                                                          $"1. Add User.\n" +
                                                          $"0. To Exit");
                                        string command = Console.ReadLine();
                                        switch (command)
                                        {
                                                case "1" :
                                                        AddUser();
                                                        break;
                                                default: break;
                                        }
                                        if (command == "0") break;
                                }
                        }
                        else
                        {
                                Console.WriteLine($"Wrong Email or password !!!!!!!!!");
                        }
                        
                }
        }
        
        private static void UserLogin()
        {
                while (true)
                {
                        Console.WriteLine($"Login User:\n" +
                                          $"Input Email: \n" +
                                          $"0 to exit. \n");
                        string email = Console.ReadLine();
                        if (email == "0") break;
                        Console.WriteLine($"Input password:");
                        string password = Console.ReadLine();
                        
                        bool ret = SQLExecutor.Exucute($"select name,email from [User] where email='{email}' AND password='{password}'");

                        if (ret)
                        {
                                Console.WriteLine($"User Logged in\n\n");
                                LoggerExecuter.Execute(new UserLoginLogger($"{email} logged in User", HelperClass.LoggerFilePath((int)LoggerFilePathEnum.UserLoginLogger)));
                                while (true)
                                {
                                        Console.WriteLine($"Welcome to User{email}\nLogin User:\n" +
                                                          $"1. Deposit Money.\n" +
                                                          $"2. Withdraw.\n" +
                                                          $"3. Transaction History.\n" +
                                                          $"0. To Exit");
                                        string command = Console.ReadLine();
                                        switch (command)
                                        {
                                                case "1" :
                                                       AddUser();
                                                        break;
                                                case "2" :
                                                        UserLogin();
                                                        break;
                                                case "0" :
                                                        ExitPrompt();
                                                        break;
                                                default: break;
                                        }
                                }
                        }
                        else
                        {
                                Console.WriteLine($"Wrong Email or password !!!!!!!!!");
                        }
                }
        }
        
        private static void ExitPrompt()
        {
                Console.WriteLine("Thanks for using BANK_TELLER\n");
        }

        private static void AddUser()
        {
                Console.WriteLine($"Add User:\n" +
                                  $"Input Email:");
                string email = Console.ReadLine();
                
                Console.WriteLine($"Input Name:");
                string name = Console.ReadLine();
                
                Console.WriteLine($"Input Contact:");
                string contact = Console.ReadLine();
                        
                Console.WriteLine($"Input password:");
                string password = Console.ReadLine();
                
                Console.WriteLine($"Input Admin or Not:");
                string adminorNot = Console.ReadLine();

                BankAccountClass bankAccountClass = new(email, name, contact, password, adminorNot == "1" ? 1 : 0);
                bool ret = bankAccountClass.SaveToDB();

                string admingOrUser = adminorNot == "1" ? "admin" : "user";
                Console.WriteLine($"User added as {admingOrUser}");
               
                
        }

        
        
}