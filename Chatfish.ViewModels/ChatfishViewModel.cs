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
    /// <summary>
    /// The view model that encompasses the entire menu screen for Chatfish
    /// </summary>
    public class ChatfishViewModel : BaseViewModel
    {
        /// <summary>
        /// The view model for the UI Panel that lets the user chat in a Knot or Catfish
        /// </summary>
        public ChatPanelViewModel ChatPanel {get; set; } = new ChatPanelViewModel();

        /// <summary>
        /// The view model for the UI Panel that lets the user view, search, and populate his or her 
        /// chats and contacts
        /// </summary>
        public SidebarViewModel SidebarPanel {get; set; }

        /// <summary>
        /// The currently displayed popup on top of the chat panel
        /// disabling all other view model's controls
        /// </summary>
        public PopupViewModel CurrentPopupViewModel { get; set; }

        public ConcreteMediator SidebarListToPopupMediator;

        public ChatfishViewModel() : base()
        {
            SidebarListToPopupMediator = new ConcreteMediator(this);
            SidebarPanel = new SidebarViewModel(SidebarListToPopupMediator);
        }
    }
}