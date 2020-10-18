namespace Chatfish.ViewModels
{
    public interface IMediator 
    {
        void Notify(BaseViewModel sender, string eventName);
    }

    public class ConcreteMediator : IMediator
    {
        ChatfishViewModel ApplicationViewModel;

        public ConcreteMediator(ChatfishViewModel applicationViewModel) 
        {
            ApplicationViewModel = applicationViewModel;  
        }

        /// <summary>
        /// Notifies the mediator that one of its participants has changed,
        /// then changes the other participant's value
        /// </summary>
        /// <param name="sender">The view model whose property changed</param>
        /// <param name="eventName">the action</param>
        public void Notify(BaseViewModel sender, string eventName)
        {
            // When the currently selected item in a sidebar list is changed
            if (sender is ContactViewModel && eventName == "change") 
            {
                ApplicationViewModel.CurrentPopupViewModel = new ContactPopupViewModel();
                ApplicationViewModel.CurrentPopupViewModel.DisplayPopup = sender != null;
                ApplicationViewModel.CurrentPopupViewModel.CurrentSidebarListItem = sender as ContactViewModel;
            } 
        }
    } 
}
