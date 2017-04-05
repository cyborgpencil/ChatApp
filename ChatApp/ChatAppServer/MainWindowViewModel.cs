using ChatAppServer.ViewModels;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatAppServer
{
    class MainWindowViewModel: BindableBase
    {

        private ObservableCollection<string> _serverStatusList;
        public ObservableCollection<string> ServerStatusList
        {
            get { return _serverStatusList; }
            set { SetProperty(ref _serverStatusList, value); }
        }
        public DelegateCommand ServerStart { get; set; }
        public DelegateCommand ServerStop { get; set; }
        private Socket serverSocketListener;
        private IPEndPoint serverEndpoint;
        private byte[] _buffer;
        private int _connectingPort;
        private UserManager UserManage;
        private ServerCommandManager _serverCommManager;
        public ManualResetEvent allDone;
        private Socket _clientSocket;
        private string _readCallbackError;

        public MainWindowViewModel()
        {
            ServerStatusList = new ObservableCollection<string>(new List<string>());
            ServerStatusList.Add("Server Ide....");
            ServerStart = new DelegateCommand(Start);
            ServerStop = new DelegateCommand(Stop);

            // Setup Server
            _connectingPort = 5555;
            allDone = new ManualResetEvent(false);

            //user 
            UserManage = new UserManager();

            // Server Command Manager
            _serverCommManager = new ServerCommandManager();

        }

        private void Stop()
        {
            ServerStatusList.Add("Server Stopped....");
        }

        private void Start()
        {
            ServerStatusList.Add("Server Started....");
            StartListening();
        }

        // performs asyc listening for server
        private void StartListening()
        {
            // incoming data buffer
            byte[] buffer = new byte[1024];
            serverSocketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverEndpoint = new IPEndPoint(IPAddress.Any, _connectingPort);
            ServerStatusList.Add("Server Listening for Connections....");

            try
            {
                serverSocketListener.Bind(serverEndpoint);
                serverSocketListener.Listen(10);

                
                serverSocketListener.BeginAccept(new AsyncCallback(Acceptcallback), serverSocketListener);

              
            }
            catch (Exception e )
            {
                Debug.Write(e.Message);
            }
            
        }

        private void Acceptcallback(IAsyncResult ar)
        {
            try
            {
                _clientSocket = serverSocketListener.EndAccept(ar);
                _buffer = new byte[_clientSocket.ReceiveBufferSize];
                _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), null);

            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }
        }

        private void ReadCallback(IAsyncResult ar)
        {
            try
            {
                
                string receivedMessage = Encoding.ASCII.GetString(_buffer,0, _clientSocket.EndReceive(ar));
                _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), null);
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }
        }
    }
}
