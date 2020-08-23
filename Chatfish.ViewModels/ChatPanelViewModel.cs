using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.Linq;
using System.IO;

namespace Chatfish.ViewModels
{
    public class ChatPanelViewModel : BaseViewModel
    {
        private bool _displayChatName;
        
        public ICommand SearchBarClickCommand {get; set; }

        public ChatViewModel CurrentChat {get; set; }

        /// <summary>
        /// Whether to display the name of the currently displayed chat
        /// </summary>
        public bool DisplayChatName 
        {
            get => _displayChatName; 
            set 
            { 
                _displayChatName = value; 

                DisplaySearchBox = !value;
                OnPropertyChanged(nameof(DisplaySearchBox));
            }
        }

        /// <summary>
        /// Whether to display the search box that searches messages in the currently displayed chat
        /// </summary>
        public bool DisplaySearchBox {get; set; }

        public ChatPanelViewModel() : base()
        {
            SearchBarClickCommand = new RelayCommand(() => DisplayChatName = !DisplayChatName);
            DisplayChatName = true;
            CurrentChat = new ChatViewModel();
        }
    }
}