namespace Chatfish.ViewModels
{
    public class SidebarListItemViewModel : BaseViewModel
    {
        /// <summary>
        /// The name the user enters to identify this entity within the SidebarList
        /// </summary>
        public string Heading {get; set; }
        
        /// <summary>
        /// The url to the picture that represents the SidebarListItem graphically
        /// </summary>
        public string IconURL {get; set; }
    }
}