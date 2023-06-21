using System.Security.Cryptography;

namespace bank_teller.Logger;

public interface ILogger
{
    void Writter();
}

public class Logger
{
    public string LogText { get; set; }
    public string FilePath { get; set; }

    public Logger(string logText,string filePath)
    {
        LogText = logText;
        FilePath = filePath;
    }
    
    public void Writter()
    {
        StreamWriter logWritter = new StreamWriter(FilePath,  append: true);
        logWritter.WriteLine($"{DateTime.Now.ToString()}: {this.LogText}");
        logWritter.Close();
    }
}

public class DBConnectLogger : Logger,ILogger
{
    public DBConnectLogger(string logText,string filePath) :  base(logText,filePath) { }
}

public class UserLoginLogger : Logger,ILogger
{
    public UserLoginLogger(string logText,string filePath) :  base(logText,filePath) { }
}

public class LoggerExecuter
{
    public static void Execute(ILogger logger)
    {
        try
        {
            logger.Writter();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}