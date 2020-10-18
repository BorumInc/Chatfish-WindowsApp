﻿using System;
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

        public MainWindow()
        {
            InitializeComponent();
            AttachConsole(-1);
            this.DataContext = new ChatfishViewModel();
            Console.WriteLine(((ChatfishViewModel) DataContext).SidebarPanel.Tank.DisplayList);
        }
    }
}
