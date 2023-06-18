// See https://aka.ms/new-console-template for more information

using System;
using System.IO;
using System.Data.SqlClient;

namespace taskSql {
    class Task {
        static void Main() {
             while(true){
                Console.WriteLine("Enter Name: ");
                string name = Console.ReadLine();

                Console.WriteLine("Enter Email: ");
                string email = Console.ReadLine();

                Console.WriteLine("Enter contact: ");
                string contact = Console.ReadLine();

                UserModel user = new UserModel(name,email,contact);
                user.saveToCSV();
                user.saveTODB();
                
            }
        }
    }

    class UserModel {
        public string name,email,contact;

        public UserModel(string name, string email, string contact) {
            this.name = name;
            this.email = email;
            this.contact = contact;
        }

        public void saveToCSV() {

            try
            {
                StreamWriter userCSV = new StreamWriter("User.csv",  append: true);
                userCSV.WriteLine($"{this.name},{this.email},{this.contact}");
                userCSV.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }finally{
                Console.WriteLine("User Updated in CSV");
            }

        }

        public void saveTODB() {

            string _connStr = @"
                Server=127.0.0.1,1433;
                Database=TestDB;
                User Id=sa;
                Password=Ashik111340_";


           using (SqlConnection connection = new SqlConnection(_connStr))
           {
                SqlCommand command = new SqlCommand("insert into persons(name,email,contact) values(@name,@email,@contact)", connection);
           
                command.Parameters.AddWithValue("@name", this.name);
                command.Parameters.AddWithValue("@email", this.email);
                command.Parameters.AddWithValue("@contact", this.contact);

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0}, {1}, {2}",
                        reader["name"], reader["email"], reader["contact"]));// etc
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
        

        }
    }

}