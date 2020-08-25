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
    public class TankViewModel : SidebarListViewModel
    {
        /// <summary>
        /// The collection of all the contacts;
        /// Never changed so as to be used as a reference for filtering
        /// </summary>
        internal readonly ObservableCollection<ContactViewModel> allTankItems;

        private ContactViewModel _currentContact;

        /// <summary>
        /// The list of contacts that are part of the user's tank
        /// </summary>
        public ObservableCollection<ContactViewModel> TankItems { get; set; }

        /// <summary>
        /// The contact that is currently being displayed in the panel;
        /// Bound to selected item of UI list that shows contacts
        /// Then gets the first result in the enumerable;
        /// </summary>
        public ContactViewModel CurrentContact 
        { 
            get => _currentContact;
            set
            {
                _currentContact = value;
                OnPropertyChanged(nameof(_currentContact));

                this.DisplayPopup = this.CurrentContact != null;
            }
        }

        /// <summary>
        /// Default constructor for TankViewModel
        /// </summary>
        public TankViewModel() : base()
        {
            // Load the initial set of tank items to both collections
            allTankItems = TankItems = LoadTankItems();
        }

        private ObservableCollection<ContactViewModel> LoadTankItems()
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
                    Name = node["Name"]?.InnerText,
                    StatusMessage = node["StatusMessage"]?.InnerText ?? "",
                    ProfilePicture = node["ProfilePicture"]?.InnerText ?? Path.Combine(
                        solutionPath, "Chatfish.Interface", "Images", "unavailable.png")
                });
            }

            return contactData;
        }
    }
}