using System.Collections.ObjectModel;
using System.Linq;

namespace Chatfish.ViewModels
{
    public abstract class SidebarListViewModel<T> : BaseViewModel
        where T : SidebarListItemViewModel, new()
    {
        internal readonly ConcreteMediator PopupMediator;
        /// <summary>
        /// The collection of all the SidebarListItem's;
        /// Never changed so as to be used as a reference for filtering
        /// </summary>
        internal readonly ObservableCollection<T> allSidebarListItems;

        private T _currentSidebarListItem;
        
        /* Public Properties */

        /// <summary>
        /// The visiblity of the sidebar list view; true when visible, false when another list is visible
        /// </summary>
        public bool DisplayList {get; set; }

        /// <summary>
        /// The list of SidebarListItem's that are part of the user's School
        /// </summary>
        public ObservableCollection<T> SidebarListItems { get; set; }

        /// <summary>
        /// The list item that is currently being displayed in the panel;
        /// Bound to selected item of UI list;
        /// Then gets the first result in the enumerable;
        /// </summary>
        public T CurrentSidebarListItem
        {
            get => _currentSidebarListItem;
            set
            {
                _currentSidebarListItem = value;
                OnPropertyChanged(nameof(_currentSidebarListItem));

                PopupMediator.Notify(CurrentSidebarListItem, "change");
            }
        }

        public SidebarListViewModel(ConcreteMediator mediator) : base()
        {
            // Load the initial set of tank items to both collections
            allSidebarListItems = SidebarListItems = LoadSidebarListItems();
            PopupMediator = mediator;
        }

        internal abstract ObservableCollection<T> LoadSidebarListItems();

        internal ObservableCollection<T> FilterSidebarList(string query)
        {
            return new ObservableCollection<T>(allSidebarListItems.Where(
                item => item.Heading.Contains(query)));
        }

    }
}