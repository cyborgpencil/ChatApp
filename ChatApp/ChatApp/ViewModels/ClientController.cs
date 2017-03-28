using ChatApp.Events;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatApp.ViewModels
{
    public class ClientController : BindableBase
    {
        #region Properties
        public bool ServerCheck { get; set; }
        public byte[] SendMessage { get; set; }
        public Socket ServerSocket { get; set; }
        public IPEndPoint ServerEndpoint { get; set; }
        public IPAddress ConnectingAddress { get; set; }
        public int ConnectingPort { get; set; }
        public ManualResetEvent connectDone { get; set; }
        public string ServerMessage { get; set; }
        private IEventAggregator _eventAggregator;
        #endregion

        #region Constructors
        public ClientController()
        {
            SendMessage = new byte[1024];
            ConnectingPort = 5555;
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ConnectingAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[2];
            ServerEndpoint = new IPEndPoint(ConnectingAddress, ConnectingPort);
            connectDone = new ManualResetEvent(true);
            _eventAggregator = new EventAggregator();
        }
        #endregion

        #region Events
        #endregion


        #region Methods
        private void CheckIfServerIsUp()
        {
            // ToDo
        }
        public void ConnectToServer()
        {
            ServerSocket.BeginConnect(ServerEndpoint, Connecting, ServerEndpoint);
            connectDone.WaitOne();
            _eventAggregator.GetEvent<UpdateServerMessageEvent>().Publish(ServerMessage);
        }

        private void Connecting(IAsyncResult ar)
        {
            try
            {
                ServerSocket.EndConnect(ar);
                ServerMessage = "Connected to Server";

                connectDone.Set();
            }
            catch (Exception e)
            {
                ServerMessage = e.Message;
            }

            _eventAggregator.GetEvent<UpdateServerMessageEvent>().Publish(ServerMessage);

        }
        #endregion
    }
}
