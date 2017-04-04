using ChatAppServer.ViewModels;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private byte[] buffer;
        private int _connectingPort;
        private UserManager UserManage;
        private ServerCommandManager _serverCommManager;
        public ManualResetEvent allDone; 

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
            serverEndpoint = new IPEndPoint(Dns.GetHostEntry("127.0.0.1").AddressList[2], _connectingPort);

            try
            {
                serverSocketListener.Bind(serverEndpoint);
                serverSocketListener.Listen(10);
                

                while(true)
                {
                    allDone.Reset();

                    ServerStatusList.Add("Server Listening for Connections....");
                    serverSocketListener.BeginAccept(new AsyncCallback(Acceptcallback), serverSocketListener);

                    allDone.WaitOne();
                }
            }
            catch (Exception e )
            {

                ServerStatusList.Add(e.Message);
            }
            
        }

        private void Acceptcallback(IAsyncResult ar)
        {
            // continue main thread
            allDone.Set();

            // Get socket handle for client request
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            byte[] buffer = new byte[1024];

            handler.BeginReceive(buffer, 0, 1024, 0, new AsyncCallback(ReadCallback), listener);
        }

        private void ReadCallback(IAsyncResult ar)
        {
            
        }
    }
}
