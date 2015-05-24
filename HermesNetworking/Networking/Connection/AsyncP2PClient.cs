using HermesNetworking.Networking.Packets;
using HermesNetworking.Networking.Packets.Handler;
using HermesNetworking.Networking.Packets.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HermesNetworking.Networking.Connection
{
    public class AsyncP2PClient
    {
        public int ClientID;
        public ushort ListenPort;

        private List<IConnection> Connections;
        private Socket serverSocket;
        private Socket clientSocket;

        public IPacketParser PacketParser { get; set; }

        public AsyncP2PClient()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Connections = new List<IConnection>();

            //ClientID = (Environment.MachineName + Process.GetCurrentProcess().Id).GetHashCode();
        }

        void AddConnection(IConnection conn)
        {
            Connections.Add(conn);
        }

        void DisconnectClient(IConnection conn)
        {
            conn.Disconnect();
            Connections.Remove(conn);
        }

        public void Broadcast(IPacket packet)
        {
            foreach (IConnection conn in Connections.ToList())
            {
                if (conn.ConnectionSocket.Connected)
                    conn.ConnectionSocket.Send(packet.GetBuffer());
            }
        }

        public void SendPacket(IConnection conn, IPacket packet)
        {
            if (conn.ConnectionSocket.Connected)
                conn.ConnectionSocket.Send(packet.GetBuffer());
        }

        #region Server-Side
        public void Start(int port, int backlog)
        {
            Bind(port);
            Listen(backlog);
            Accept();
        }

        public void Bind(int port)
        {
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            this.ListenPort = (ushort)port;
        }

        public void Listen(int backlog)
        {
            serverSocket.Listen(backlog);
        }

        public void Accept()
        {
            serverSocket.BeginAccept(new AsyncCallback(AcceptedCallback), serverSocket);
        }

        private void AcceptedCallback(IAsyncResult result)
        {
            Socket connSocket = serverSocket.EndAccept(result);
            IConnection conn = OnNewConnection(connSocket);
            AddConnection(conn);
            conn.GetReady();
            Accept();
        }


        #endregion

        protected virtual IConnection OnNewConnection(Socket newClientSocket)
        {
            IConnection conn = new AsyncConnection(newClientSocket, new PacketHandler(PacketParser));
            return conn;
        }

        #region Client-side
        public void Connect(string ip, int port)
        {
            clientSocket.BeginConnect(new IPEndPoint(IPAddress.Parse(ip), port), ConnectedCallback, clientSocket);
        }

        private void ConnectedCallback(IAsyncResult result)
        {
            IConnection conn = OnNewConnection(result.AsyncState as Socket);
            AddConnection(conn);
            conn.GetReady();
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        #endregion
    }
}
