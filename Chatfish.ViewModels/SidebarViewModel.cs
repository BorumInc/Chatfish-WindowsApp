using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Reflection;

namespace Chatfish.ViewModels
{
    /// <summary>
    /// The currently displayed list
    /// </summary>
    public enum SidebarState
    {
        Contacts,
        Chats,
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
        public SidebarState CurrentListState {get; set; } = SidebarState.Contacts;
        
        /// <summary>
        /// The set of data for the Tank, the contacts list
        /// </summary>
        public TankViewModel Tank { get; set; }

        /// <summary>
        /// The set of data for the School, the chats list
        /// </summary>
        /// <returns></returns>
        public SchoolViewModel School {get; set; }

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

                switch (this.CurrentListState)
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

        public SidebarViewModel(ConcreteMediator mediator) : base()
        {
            SwitchStateCommand = new RelayCommand(() => 
            {
                switch (CurrentListState)
                {
                    case SidebarState.Chats:
                        CurrentListState = SidebarState.Contacts;
                        School.DisplayList = false;
                        Tank.DisplayList = true;
                        break;
                    case SidebarState.Contacts:
                        CurrentListState = SidebarState.Chats;
                        School.DisplayList = true;
                        Tank.DisplayList = false;
                        break;
                }
            });

            InitializeSidebarLists(mediator);
        }

        private void InitializeSidebarLists(ConcreteMediator mediator)
        {
            Tank = new TankViewModel(mediator);
            Tank.DisplayList = CurrentListState == SidebarState.Contacts;
            School = new SchoolViewModel(mediator);
            School.DisplayList = CurrentListState == SidebarState.Chats;
        }
    }
}