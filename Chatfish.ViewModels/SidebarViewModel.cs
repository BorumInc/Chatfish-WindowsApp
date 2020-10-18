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
                
                dynamic sidebarList = QuerySearch();
                OnPropertyChanged(nameof(sidebarList.SidebarListItems));                
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
        /// The command to change the sidebar state to chats
        /// </summary>
        public ICommand SwitchToChatsCommand {get; set; }

        /// <summary>
        /// The command to change the Sidebar State to contacts
        /// </summary>
        public ICommand SwitchToContactsCommand {get; set; }

        public SidebarViewModel(ConcreteMediator mediator) : base()
        {
            SwitchToChatsCommand = new RelayCommand(() => 
            {
                CurrentListState = SidebarState.Chats;
                School.DisplayList = true;
                Tank.DisplayList = false;
            });

            SwitchToContactsCommand = new RelayCommand(() => 
            {
                CurrentListState = SidebarState.Contacts;
                School.DisplayList = false;
                Tank.DisplayList = true;
            });

            InitializeSidebarLists(mediator);
        }

        /* Helper Methods */

        private void InitializeSidebarLists(ConcreteMediator mediator)
        {
            Tank = new TankViewModel(mediator);
            Tank.DisplayList = CurrentListState.Equals(SidebarState.Contacts);
            School = new SchoolViewModel(mediator);
            School.DisplayList = CurrentListState.Equals(SidebarState.Chats);
        }

        /// <summary>
        /// Determine search results of SearchQuery 
        /// by dynamically getting the current sidebar list's property name,
        /// then calling FilterSidebarList on that property
        /// </summary>
        /// <returns>The object that contains the search query results</returns>
        private dynamic QuerySearch()
        {
            switch(CurrentListState)
            {
                case SidebarState.Contacts:
                    Tank.SidebarListItems = Tank.FilterSidebarList(SearchQuery);
                    return Tank.SidebarListItems;
                case SidebarState.Chats:
                    School.SidebarListItems = School.FilterSidebarList(SearchQuery);
                    return School.SidebarListItems;
                default:
                    return null;
            }
        }
    }
}