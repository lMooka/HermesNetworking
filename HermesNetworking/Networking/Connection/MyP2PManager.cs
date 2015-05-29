using HermesNetworking.Networking.Packets;
using HermesNetworking.Networking.Packets.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HermesNetworking.Networking.Connection
{
    public class MyP2PManager : IMyConnectionManager
    {
        private IMyPacketHandler _MyPacketHandler;

        public List<IMyConnection> MyConnections { get; private set; }
        public IMyConnector MyConnector { get; set; }
        public IMyListener MyListener { get; set; }
        public IMyPacketHandler MyPacketHandler
        {
            get
            {
                return _MyPacketHandler;
            }
            set
            {
                _MyPacketHandler = value;
                foreach (IMyConnection connection in MyConnections)
                    connection.MyPacketHandler = value;
            }
        }

        public MyP2PManager(IMyConnector connector, IMyListener listener, IMyPacketHandler handler)
        {
            MyConnections = new List<IMyConnection>();
            MyConnector = connector;
            MyListener = listener;
            MyPacketHandler = handler;
        }

        public void DisconnectClient(IMyConnection conn)
        {
            conn.Disconnect();
            MyConnections.Remove(conn);
        }

        public void Broadcast(IMyPacket packet)
        {
            foreach (IMyConnection conn in MyConnections.ToList())
            {
                if (conn.MySocket.Connected)
                    conn.MySocket.Send(packet.GetBuffer());
            }
        }

        public void SendPacket(IMyConnection conn, IMyPacket packet)
        {
            if (conn.MySocket.Connected)
                conn.MySocket.Send(packet.GetBuffer());
        }

        void IMyConnectionManager.NotifyNewConnection(IMyConnection connection)
        {
            MyConnections.Add(connection);
        }
    }
}
