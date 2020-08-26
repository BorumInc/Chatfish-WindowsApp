using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Chatfish.ViewModels
{
    /// <summary>
    /// The currently displayed list
    /// </summary>
    public enum SidebarState
    {
        Contacts,
        Chats
    }

    /// <summary>
    /// The view model that represents the sidebar which encompasses 
    /// the contact/chat search bar, 
    /// the TankList and SchoolList,
    /// the controls for creating new chats and contacts
    /// </summary>
    public class SidebarViewModel : BaseViewModel
    {
        /* Private Instance Variables */
        private string _searchQuery = "";
        
        /* Public Properties */

        /// <summary>
        /// The current list content type that the user has selected
        /// Defaults to Contacts
        /// </summary>
        public SidebarState CurrentList {get; set; } = SidebarState.Contacts;
        
        /// <summary>
        /// The set of data for the Tank, the contacts list
        /// </summary>
        public TankViewModel Tank { get; set; } = new TankViewModel();

        /// <summary>
        /// The set of data for the School, the chats list
        /// </summary>
        /// <returns></returns>
        public SchoolViewModel School {get; set; } = new SchoolViewModel();

        /// <summary>
        /// The text used to filter out the currently displayed contacts in the tank list
        /// or currently displayed chats in the school list
        /// </summary>
        public string SearchQuery 
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(_searchQuery));

                switch (this.CurrentList)
                {
                    case SidebarState.Chats:
                        Tank.SidebarListItems = Tank.FilterSidebarList(SearchQuery);
                        OnPropertyChanged(nameof(Tank.SidebarListItems));
                        break;
                    case SidebarState.Contacts:
                        School.SidebarListItems = School.FilterSidebarList(SearchQuery);
                        OnPropertyChanged(nameof(School.SidebarListItems));
                        break;
                }
                
            } 
        }

        /* Commands */

        /// <summary>
        /// The command to create a new Catfish (a chat)
        /// </summary>
        public ICommand NewCatfishCommand;

        /// <summary>
        /// The command to create a new Knot (a chat)
        /// </summary>
        public ICommand NewKnotCommand;
        
        /// <summary>
        /// The command to change what the list is currently displaying
        /// </summary>
        public ICommand SwitchStateCommand;

        public SidebarViewModel() : base()
        {
            SwitchStateCommand = new RelayCommand(() => 
            {
                switch (CurrentList)
                {
                    case SidebarState.Chats:
                        CurrentList = SidebarState.Contacts;
                        break;
                    case SidebarState.Contacts:
                        CurrentList = SidebarState.Chats;
                        break;
                }
            });
        }
    }
}