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
    public class TankViewModel : BaseViewModel
    {
        private ObservableCollection<ContactViewModel> allTankItems;
        private ContactViewModel _currentContact;
        private string _searchQuery = "";

        /// <summary>
        /// The list of contacts that are part of the user's tank
        /// </summary>
        public ObservableCollection<ContactViewModel> TankItems { get; set; }

        /// <summary>
        /// The contact that is currently being displayed in the panel;
        /// Determined by filtering all TankItem's into only where its DisplayPopup property is true,
        /// Then gets the first result in the enumerable;
        /// Empty constructor when one isn't being displayed
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

        // Raises INotifyPropertyChanged.PropertyChanged
        public bool DisplayPopup { get; set; }

        /// <summary>
        /// The text used to filter out the currently displayed contacts in the tank list
        /// </summary>
        public string SearchQuery 
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(_searchQuery));

                TankItems = new ObservableCollection<ContactViewModel>(allTankItems.Where(item => item.Name.Contains(_searchQuery)));
                OnPropertyChanged(nameof(TankItems));
            } 
        }

        public ICommand ClosePopupCommand { get; set; }

        public TankViewModel() : base()
        {
            allTankItems = LoadTankItems();
            TankItems = LoadTankItems();
            ClosePopupCommand = new RelayCommand(() => 
            {
                DisplayPopup = false;
                OnPropertyChanged(nameof(DisplayPopup));
            });
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