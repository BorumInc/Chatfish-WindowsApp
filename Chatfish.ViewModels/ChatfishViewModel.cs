using System;

namespace Chatfish.ViewModels
{
    public class ChatfishViewModel : BaseViewModel
    {
        public TankViewModel Contacts { get; set; }

        public ChatfishViewModel() : base()
        {
            
        }
    }
}