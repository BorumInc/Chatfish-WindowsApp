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
        private ContactViewModel _currentContact;

        /// <summary>
        /// The list of contacts that are part of the user's tank
        /// </summary>
        public ObservableCollection<ContactViewModel> TankItems { get; set; }

        /// <summary>
        /// The contact that is currently being displayed in the panel
        /// Determined by filtering all TankItem into only where its DisplayPopup property is true
        /// Then gets the first result in the enumerable
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

        public ICommand ClosePopupCommand { get; set; }

        // Raises INotifyPropertyChanged.PropertyChanged
        public bool DisplayPopup { get; set; }

        public TankViewModel() : base()
        {
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