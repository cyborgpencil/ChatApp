using ChatApp.Models;
using Prism;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private ChatUser _charUser;
        public ChatUser CharUser
        {
            get { return _charUser; }
            set { SetProperty(ref _charUser, value); }
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get { return _statusMessage; }
            set { SetProperty(ref _statusMessage, value); }
        }
        public MainWindowViewModel()
        {
            CharUser = new ChatUser();
            StatusMessage = "Connecting to Server...";

        }
    }
}
