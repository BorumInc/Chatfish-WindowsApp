using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
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
            var contactData = new ObservableCollection<ContactViewModel>() {
            
                new ContactViewModel(
                    "James Earl Jones", 
                    "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/96b007a7-e86d-465f-a231-fcb646b3625e/d30j6xh-9269b2eb-06c3-4c70-a44b-2e4b63342006.png?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwic3ViIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsImF1ZCI6WyJ1cm46c2VydmljZTpmaWxlLmRvd25sb2FkIl0sIm9iaiI6W1t7InBhdGgiOiIvZi85NmIwMDdhNy1lODZkLTQ2NWYtYTIzMS1mY2I2NDZiMzYyNWUvZDMwajZ4aC05MjY5YjJlYi0wNmMzLTRjNzAtYTQ0Yi0yZTRiNjMzNDIwMDYucG5nIn1dXX0.xKiSsqS74EKXxW_nSh6995qJmEX0AqhZm28ho9pv0Dc",
                    "Shaking my head at the Lion King remake"
                ),
                new ContactViewModel(
                    "John", 
                    "https://www.runscope.com/static/img/public/customer-portrait-edmunds.png", 
                    "Working from home"
                ),
                new ContactViewModel(
                    "Lisa",
                    "https://toppng.com/uploads/thumbnail/lisa-simpson-lisa-from-the-simpsons-head-11563176790zlr4ctvni6.png",
                    "Co-heading the Simpson family"
                )
            };

            return contactData;
        }
    }
}