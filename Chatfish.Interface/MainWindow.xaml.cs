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
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using Chatfish.ViewModels;

namespace Chatfish.Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        [DllImport("Kernel32.dll")]
        public static extern bool AttachConsole(int processId);
        private BooleanToVisibilityConverter hiddenOrVisible;

        public MainWindow()
        {
            InitializeComponent();
            AttachConsole(-1);
            hiddenOrVisible = new BooleanToVisibilityConverter();
            this.DataContext = new TankViewModel();
            Console.WriteLine(((TankViewModel) DataContext).CurrentContact);
        }

        private void TankList_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // DONE DisplayPopup properties are correct
            // DONE CurrentContact is correct
            bool displayPopupVal = ((TankViewModel) DataContext).DisplayPopup;
            Console.WriteLine(hiddenOrVisible.Convert(displayPopupVal, typeof(Visibility), null, null));
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(ChatList.ItemsSource);
            // Apply the filter
            Console.WriteLine(cv);
            cv.Filter = item =>
            {
                XmlElement readableElement = item as XmlElement;
                String name = readableElement.GetAttribute("Name");
                return name.Contains(((TextBox)sender).Text);
            };
        }

        private void SearchButton_Click(object sender, MouseButtonEventArgs e)
        {
            bool searchBoxIsVisible = MessageSearchBox.Visibility == Visibility.Visible;
            Visibility searchBoxVisibility = (Visibility)hiddenOrVisible.Convert(!searchBoxIsVisible, typeof(Visibility), null, null);
            MessageSearchBox.Visibility = searchBoxVisibility;
            ChatName.Visibility = searchBoxIsVisible ? Visibility.Visible : Visibility.Hidden;
        }

        private void SearchButton_Hover(object sender, RoutedEventArgs e)
        {
            ((Canvas)sender).Cursor = Cursors.Hand;
            foreach (Shape item in ((Canvas)sender).Children)
            {
                item.Stroke = Brushes.Black;
            }
        }

        private void SearchButton_Leave(object sender, RoutedEventArgs e)
        {
            foreach (Shape item in ((Canvas)sender).Children)
            {
                item.Stroke = Brushes.Gray;
            }
        }
    }
}
