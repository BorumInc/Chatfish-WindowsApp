using System;

namespace Templates.WPFLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args[0].StartsWith("loadpage-"))
            {
                string className = args[0].Substring("loadpage-".Length);
                /**                
                    <Page x:Class="ProjectName.ClassName"
                    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
                    xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
                    mc:Ignorable="d" 
                    d:DesignHeight="350" d:DesignWidth="500"
                    Title="">
                        <Grid>
                            
                        </Grid>
                    </Page>
                */
                string xamlOutput = String.Join(
                    Environment.NewLine,
                    $"<Page x:Class=\"{className}\"",
                    "xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"",
                    "xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"",
                    "xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\"",
                    "xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\"",
                    "mc:Ignorable=\"d\"",
                    "d:DesignHeight=\"350\" d:DesignWidth=\"500\"",
                    $"Title=\"\">",
                    "<Grid>", "", "</Grid>",
                    "</Page>");
                Console.WriteLine(xamlOutput);
            } 
            else
            {
                Console.WriteLine("You must enter the argument in the following format: ");
                Console.Write("loadpage-CLASSNAME");
            }
        }
    }
}
