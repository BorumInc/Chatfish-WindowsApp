using System;
using System.Text;
using MySql.Data.MySqlClient;

namespace Chatfish.Aquarium
{
    public class Class1
    {
        public void StartConnection()
        {
            String server = "160.153.59.192";
            String database = "Chatfish";
            String uid = "libuc6kfb0jg";
            String password = "LearnPhp@@@1";
            String connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            using (MySqlConnection connection = new MySqlConnection(
                 connectionString))
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM messages", connection);
                // Allow for encoding 1252
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                command.Connection.Open();
                Console.WriteLine($"Connection connected to {database}");
                MySqlDataReader myReader = command.ExecuteReader();
                try
                {
                    while (myReader.Read())
                    {
                        Console.WriteLine(myReader.GetString(1));
                    }
                }
                finally
                {
                    myReader.Close();
                    connection.Close();
                }
            }
        }
    }
}
