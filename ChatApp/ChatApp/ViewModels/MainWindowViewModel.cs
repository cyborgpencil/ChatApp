using ChatApp.Events;
using ChatApp.Models;
using Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        

        private string _statusMessage;
        public string StatusMessage
        {
            get { return _statusMessage; }
            set { SetProperty(ref _statusMessage, value); }
        }
        private string _chatText;
        public string ChatText
        {
            get { return _chatText; }
            set { SetProperty(ref _chatText, value); }
        }

        //Chat Log
        private ObservableCollection<string> _currentChatLog;
        public ObservableCollection<string> CurrentChatLog
        {
            get { return _currentChatLog; }
            set { SetProperty(ref _currentChatLog, value); }
        }
        private string _textChatFocus;
        public string TextChatFocus
        {
            get { return _textChatFocus; }
            set { SetProperty(ref _textChatFocus, value); }
        }
        private string _username;
        public string UserName
        {
            get { return _username; }
            set { SetProperty(ref _username, value);
                ChangeUsername();
            }
        }

        private bool _userNameChanged;
        private string _oldUsername;

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
        private readonly IRegionManager _regionManager;
        public DelegateCommand LoadedCommand { get; set; }

        public MainWindowViewModel() 
        {
            ClientController = new ClientController();
            SubmitCommand = new DelegateCommand(TextSubmit);
            LoadedCommand = new DelegateCommand(Loaded);
            CurrentChatLog = new ObservableCollection<string>(new List<string>());
            TextChatFocus = "TextChat";

            // Set Default username
            UserName = SetUserName();
        }


        public MainWindowViewModel(IEventAggregator eventAggregator,IRegionManager regionManager) :this()
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
        }

        private void UpdateMessage(string obj)
        {
            StatusMessage = obj;
        }

        private void Loaded()
        {
            _regionManager.RequestNavigate("StatusRegion", "ServerStatusView");
        }

        private void TextSubmit()
        {
            // make sure message is not blank
            if(!string.IsNullOrWhiteSpace(ChatText))
            {
                // Check for username change
                if(_userNameChanged)
                {
                    CurrentChatLog.Add($"{_oldUsername} changed Name to {UserName}");
                    _oldUsername = string.Empty;
                    _userNameChanged = false;
                }
                CurrentChatLog.Add($"{UserName} : {ChatText.ToString()}");
            }

            //Server
             _eventAggregator.GetEvent<UpdateServerMessageEvent>().Subscribe(UpdateMessage);
             ClientController.ConnectToServer();

            // Clear Chat message
            ChatText = string.Empty;
            TextChatFocus = "TextChat";

        }

        private string SetUserName()
        {
            return "User01";
        }

        private void ChangeUsername()
        {
            _oldUsername = UserName;
            _userNameChanged = true;
        }
    }
}
