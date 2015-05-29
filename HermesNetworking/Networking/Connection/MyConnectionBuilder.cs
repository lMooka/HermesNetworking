using HermesNetworking.Networking.Packets.Handler;
using System.Net.Sockets;

namespace HermesNetworking.Networking.Connection
{
    public abstract class MyConnectionBuilder
    {
        public abstract IMyConnection Create(Socket socket, IMyPacketHandler handler);
    }
}
