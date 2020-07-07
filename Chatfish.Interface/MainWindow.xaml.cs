using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;

namespace Chatfish.Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("Kernel32.dll")]
        public static extern bool AttachConsole(int processId);

        public MainWindow()
        {
            InitializeComponent();
            AttachConsole(-1);
            StartConnection();
        }

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
