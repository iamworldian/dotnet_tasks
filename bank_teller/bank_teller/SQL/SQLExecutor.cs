using System.Data.SqlClient;
using bank_teller.Helpers;
using bank_teller.Logger;

namespace bank_teller.SQL;

public class SQLExecutor
{
    public static bool Exucute(string sql)
    {
        string connStr = @"Server=127.0.0.1,1433;Database=testdb;User Id=sa;Password=Ashik111340_;TrustServerCertificate=True;";
        SqlConnection connection = new SqlConnection(connStr);
        bool ret = false;
        Console.WriteLine(sql);
        try
        {
            connection.Open();
            LoggerExecuter.Execute(new DBConnectLogger($"DB Opened",HelperClass.LoggerFilePath((int)LoggerFilePathEnum.DBConnectLogger)));
            

            SqlCommand command = new(sql, connection);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            ret = sqlDataReader.HasRows;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            connection.Close();
            LoggerExecuter.Execute(new DBConnectLogger($"DB Closed",HelperClass.LoggerFilePath((int)LoggerFilePathEnum.DBConnectLogger)));
        }
        return ret;
    }
}