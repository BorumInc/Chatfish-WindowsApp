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
    public class ChatfishViewModel : BaseViewModel
    {
        public TankViewModel Tank { get; set; } = new TankViewModel();

        public ChatPanelViewModel ChatPanel {get; set; } = new ChatPanelViewModel();

        public ChatfishViewModel() : base()
        {
        }
    }
}