using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HermesNetworking.Networking.Connection
{
    public class MyAsyncTCPListener : IMyListener
    {
        public int Port { get; private set; }
        public int Backlog { get; private set;  }
        public IMyConnectionManager MyConnectionManager { get; set; }
        public MyConnectionBuilder MyConnectionBuilder { get; set; }
        private Socket MySocketListener { get; set; }

        public MyAsyncTCPListener(IMyConnectionManager manager, MyConnectionBuilder builder)
        {
            this.MyConnectionBuilder = builder;
            MySocketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Listen(int port, int backlog)
        {
            this.Port = port;
            this.Backlog = backlog;

            MySocketListener.Bind(new IPEndPoint(IPAddress.Any, port));
            MySocketListener.Listen(backlog);
            MySocketListener.BeginAccept(new AsyncCallback(OnNewConnection), MySocketListener);
        }

        private void OnNewConnection(IAsyncResult result)
        {
            Socket socket = MySocketListener.EndAccept(result);
            MySocketListener.BeginAccept(new AsyncCallback(OnNewConnection), MySocketListener);
            MyConnectionManager.NotifyNewConnection(MyConnectionBuilder.Create(socket, MyConnectionManager.MyPacketHandler));
        }
    }
}
