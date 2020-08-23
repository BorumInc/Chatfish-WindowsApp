namespace Chatfish.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        public string Name {get; set; } = "The Three Musketeers";
        
        public string ProfilePicture {get; set; }
        
        public ChatViewModel() : base()
        {

        }
    }
}