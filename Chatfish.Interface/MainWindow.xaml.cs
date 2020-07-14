using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.ComponentModel;
using System.Runtime.InteropServices;
namespace Chatfish.Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("Kernel32.dll")]
        public static extern bool AttachConsole(int processId);

        private ObservableCollection<string> _names = new ObservableCollection<string>()
        {
            "Isabel", "Michal"
        };

        public ObservableCollection<string> Names
        {
            get { return _names; }
            set { _names = value; }
        }

        private ICollectionView View;

        public MainWindow()
        {
            InitializeComponent();
            AttachConsole(-1);
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            View.Filter = x => x.ToString().ToLower().Contains(((TextBox)sender).Text.ToLower());
        }
    }
}
