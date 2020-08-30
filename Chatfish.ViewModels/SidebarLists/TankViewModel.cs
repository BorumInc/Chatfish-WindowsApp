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
    /// The view that displays all of the user's contacts (their tank)
    /// </summary>
    public class TankViewModel : SidebarListViewModel<ContactViewModel>
    {
        public TankViewModel(ConcreteMediator mediator) : base(mediator) {}   

        /// <summary>
        /// Loads all the Tank items (contacts) from the XML file
        /// </summary>
        /// <returns>The list of contacts</returns>
        internal override ObservableCollection<ContactViewModel> LoadSidebarListItems()
        {
            var contactData = new ObservableCollection<ContactViewModel>();

            string solutionPath =  Directory.GetParent(
                Directory.GetCurrentDirectory()).FullName;
            string fileName = Path.Combine(solutionPath, "Chatfish.Aquarium", "Contacts.xml");

            XmlDocument doc = new XmlDocument();

            doc.Load(fileName);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("Contact"); 

            foreach (XmlElement node in nodes)
            {
                contactData.Add(new ContactViewModel()
                {
                    Heading = node["Name"]?.InnerText,
                    StatusMessage = node["StatusMessage"]?.InnerText ?? "",
                    IconURL = node["ProfilePicture"]?.InnerText ?? Path.Combine(
                        solutionPath, "Chatfish.Interface", "Images", "unavailable.png")
                });
            }

            return contactData;
        }
    }
}