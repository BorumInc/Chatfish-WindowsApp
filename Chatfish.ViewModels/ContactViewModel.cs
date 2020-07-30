using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Chatfish.ViewModels
{
    public class ContactViewModel : BaseViewModel
    {
        private string _statusMessage;
        public string Name {get; set; }
        public string ProfilePicture {get; set; }
        public string StatusMessage {
            get => _statusMessage;
            set => _statusMessage = "\"" + value + "\"";
        }

        /// <summary>
        /// Whether this will be displayed
        /// Defaults to false
        /// </summary>
        public bool DisplayPanel { get; set; } = false;

        public ContactViewModel()
        {
            
        }
    }
}