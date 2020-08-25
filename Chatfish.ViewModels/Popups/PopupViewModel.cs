using System.Windows.Input;

namespace Chatfish.ViewModels
{
    public abstract class PopupViewModel : BaseViewModel
    {
        // Raises INotifyPropertyChanged.PropertyChanged
        public bool DisplayPopup { get; set; }

        public ICommand ClosePopupCommand { get; set; }

        public PopupViewModel() : base() 
        {
            ClosePopupCommand = new RelayCommand(() => 
            {
                DisplayPopup = false;
                OnPropertyChanged(nameof(DisplayPopup));
            });
        }
    }
}