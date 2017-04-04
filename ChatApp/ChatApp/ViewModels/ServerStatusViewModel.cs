using System;
using Prism.Unity;
using Prism.Mvvm;
using ChatApp.Models;

namespace ChatApp.ViewModels
{
    public class ServerStatusViewModel : BindableBase
    {
        private string _statusMessage;
        public string StatusMessage
        {
            get { return _statusMessage; }
            set { SetProperty(ref _statusMessage, value); }
        }
        private ChatUser _charUser;
        public ChatUser CharUser
        {
            get { return _charUser; }
            set { SetProperty(ref _charUser, value); }
        }

        public ServerStatusViewModel()
        {
            CharUser = new ChatUser();
            StatusMessage = "Test";
        }
    }
}
