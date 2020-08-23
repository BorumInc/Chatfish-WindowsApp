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
    public class SchoolViewModel : BaseViewModel
    {
        /// <summary>
        /// The collection of all the Chats;
        /// Never changed so as to be used as a reference for filtering
        /// </summary>
        private readonly ObservableCollection<ChatViewModel> allSchoolItems;

        private ChatViewModel _currentChat;
        private string _searchQuery = "";

        /// <summary>
        /// The list of Chats that are part of the user's School
        /// </summary>
        public ObservableCollection<ChatViewModel> SchoolItems { get; set; }

        /// <summary>
        /// The Chat that is currently being displayed in the panel;
        /// Bound to selected item of UI list that shows Chats
        /// Then gets the first result in the enumerable;
        /// </summary>
        public ChatViewModel CurrentChat 
        { 
            get => _currentChat;
            set
            {
                _currentChat = value;
                OnPropertyChanged(nameof(_currentChat));

                this.DisplayPopup = this.CurrentChat != null;
            }
        }

        // Raises INotifyPropertyChanged.PropertyChanged
        public bool DisplayPopup { get; set; }

        /// <summary>
        /// The text used to filter out the currently displayed Chats in the School list
        /// </summary>
        public string SearchQuery 
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(_searchQuery));

                SchoolItems = new ObservableCollection<ChatViewModel>(allSchoolItems.Where(item => item.Name.Contains(_searchQuery)));
                OnPropertyChanged(nameof(SchoolItems));
            } 
        }

        public ICommand ClosePopupCommand { get; set; }

        public SchoolViewModel() : base()
        {
            allSchoolItems = LoadSchoolItems();
            SchoolItems = LoadSchoolItems();
            ClosePopupCommand = new RelayCommand(() => 
            {
                DisplayPopup = false;
                OnPropertyChanged(nameof(DisplayPopup));
            });
        }

        private ObservableCollection<ChatViewModel> LoadSchoolItems()
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
                    Name = node["Name"]?.InnerText,
                    ProfilePicture = node["ProfilePicture"]?.InnerText ?? Path.Combine(
                        solutionPath, "Chatfish.Interface", "Images", "unavailable.png")
                });
            }

            return ChatData;
        }

    }
}