using ChatApp.Events;
using ChatApp.Models;
using Prism;
using Prism.Commands;
using Prism.Events;
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
        private ClientController _clientController;
        public ClientController ClientController
        {
            get { return _clientController; }
            set { SetProperty(ref _clientController, value); }
        }
        private IEventAggregator _eventAggregator;
        private DelegateCommand _submitCommand;
        public DelegateCommand SubmitCommand
        {
            get { return _submitCommand; }
            set { SetProperty(ref _submitCommand, value); }
        }

        public MainWindowViewModel() 
        {
          

        }

        public MainWindowViewModel(IEventAggregator eventAggregator) :this()
        {
            ClientController = new ClientController();
            SubmitCommand = new DelegateCommand(CheckServer);

            CharUser = new ChatUser();
            StatusMessage = "Connecting to Server...";

            _eventAggregator = eventAggregator;
           
        }

        private void UpdateMessage(string obj)
        {
            StatusMessage = obj;
        }

        private void CheckServer()
        {
            _eventAggregator.GetEvent<UpdateServerMessageEvent>().Subscribe(UpdateMessage);
            ClientController.ConnectToServer();
        }
    }
}
