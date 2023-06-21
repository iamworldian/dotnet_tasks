namespace bank_teller.Helpers;


public enum LoggerFilePathEnum
{
    UserLoginLogger = 0,
    DBConnectLogger = 1,
                
}

public class HelperClass
{
    
    public static string LoggerFilePath(int index)
    {
        string[] paths = { "UserLoginLogger.txt", "DBConnectLogger.txt" };
        return paths[index];
    }
}