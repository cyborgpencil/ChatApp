
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatApp.ViewModels
{
    public class ClientController
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
        #endregion

        #region Constructors
        public ClientController()
        {
            SendMessage = new byte[1024];
            ConnectingPort = 5555;
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ConnectingAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
            ServerEndpoint = new IPEndPoint(ConnectingAddress, ConnectingPort);
            connectDone = new ManualResetEvent(true);
        }
        #endregion

        #region Events
        #endregion


        #region Methods
        private void CheckIfServerIsUp()
        {
            // ToDo
        }
        private void ConnectToServer()
        {
            ServerSocket.BeginConnect(ServerEndpoint, Connecting, ServerEndpoint);
            connectDone.WaitOne();
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
        }
        #endregion
    }
}
