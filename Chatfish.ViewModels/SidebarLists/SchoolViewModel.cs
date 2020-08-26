using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.Linq;
using System.IO;

namespace Chatfish.ViewModels
{
    /// <summary>
    /// The view that displays all of the user's chats (Knots and Catfish)
    /// </summary>
    public class SchoolViewModel : SidebarListViewModel<ChatViewModel>
    {
        internal override ObservableCollection<ChatViewModel> LoadSidebarListItems()
        {
            var ChatData = new ObservableCollection<ChatViewModel>();

            string solutionPath =  Directory.GetParent(
                Directory.GetCurrentDirectory()).FullName;
            string fileName = Path.Combine(solutionPath, "Chatfish.Aquarium", "Chats.xml");

            XmlDocument doc = new XmlDocument();

            doc.Load(fileName);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("Chat"); 

            foreach (XmlElement node in nodes)
            {
                ChatData.Add(new ChatViewModel()
                {
                    Heading = node["Name"]?.InnerText,
                    IconURL = node["ProfilePicture"]?.InnerText ?? Path.Combine(
                        solutionPath, "Chatfish.Interface", "Images", "unavailable.png")
                });
            }

            return ChatData;
        }
    }
}