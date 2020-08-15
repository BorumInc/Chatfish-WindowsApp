using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;

namespace Chatfish.ViewModels
{
    public class ContactViewModel : BaseViewModel
    {
        private string _statusMessage;

        /// <summary>
        /// The name the user entered that identifies him or her
        /// </summary>
        public string Name {get; set; }

        /// <summary>
        /// The url to the user's profile picture
        /// </summary>
        public string ProfilePicture {get; set; }
        
        /// <summary>
        /// The user's status message
        /// </summary>
        public string StatusMessage 
        {
            get => _statusMessage;
            set => _statusMessage = "\"" + value + "\"";
        }

        /// <summary>
        /// Whether the user has selected the current contact in the list
        /// Default to false
        /// </summary>
        public bool IsSelected { get; set; }

        public int FontSize { get; set; } = 20;

        public ContactViewModel() : base() 
        {
            
        }

        public ContactViewModel(Dictionary<string, dynamic> data) : base()
        {
            dynamic val;
            data.TryGetValue("Name", out val);
            Name = val as string;
        }

        public ContactViewModel(string name, string profilePicture, string status)
        {
            Name = name;
            ProfilePicture = profilePicture;
            StatusMessage = status;
        }
    }
}