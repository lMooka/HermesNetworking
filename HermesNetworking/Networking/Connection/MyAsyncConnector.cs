using System;
using System.Net;
using System.Net.Sockets;


namespace HermesNetworking.Networking.Connection
{
    public class MyAsyncTCPConnector : IMyConnector
    {
        private Socket MySocket { get; set; }
        public IMyConnectionManager MyConnectionManager { get; set; }
        public MyConnectionBuilder MyConnectionBuilder { get; set; }

        public void Connect(string ip, int port)
        {
            MySocket.BeginConnect(new IPEndPoint(IPAddress.Parse(ip), port), OnConnected, MySocket);
        }

        private void OnConnected(IAsyncResult result)
        {
            IMyConnection connection = MyConnectionBuilder.Create((result as Socket), MyConnectionManager.MyPacketHandler);
            MySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            MyConnectionManager.NotifyNewConnection(connection);
        }

        void IMyConnector.Connect(string ip, int port)
        {
            MySocket.BeginConnect(new IPEndPoint(IPAddress.Parse(ip), port), OnConnected, MySocket);
        }
    }
}
