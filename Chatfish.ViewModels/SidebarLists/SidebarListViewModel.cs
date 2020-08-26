using System.Collections.ObjectModel;
using System.Linq;

namespace Chatfish.ViewModels
{
    public abstract class SidebarListViewModel<T> : BaseViewModel
        where T : SidebarListItemViewModel, new()
    {
        /// <summary>
        /// The collection of all the SidebarListItem's;
        /// Never changed so as to be used as a reference for filtering
        /// </summary>
        internal readonly ObservableCollection<T> allSidebarListItems;

        private T _currentSidebarListItem;

        /// <summary>
        /// The list of SidebarListItem's that are part of the user's School
        /// </summary>
        public ObservableCollection<T> SidebarListItems { get; set; }

        /// <summary>
        /// The contact that is currently being displayed in the panel;
        /// Bound to selected item of UI list that shows contacts
        /// Then gets the first result in the enumerable;
        /// </summary>
        public T CurrentSidebarListItem
        {
            get => _currentSidebarListItem;
            set
            {
                _currentSidebarListItem = value;
                OnPropertyChanged(nameof(_currentSidebarListItem));

                this.DisplayPopup = this.CurrentSidebarListItem != null;
            }
        }

        public SidebarListViewModel() : base()
        {
            // Load the initial set of tank items to both collections
            allSidebarListItems = SidebarListItems = LoadSidebarListItems();;
        }

        internal abstract ObservableCollection<T> LoadSidebarListItems();

        internal ObservableCollection<T> FilterSidebarList(string query)
        {
            return new ObservableCollection<T>(allSidebarListItems.Where(
                item => item.Heading.Contains(query)));
        }

    }
}