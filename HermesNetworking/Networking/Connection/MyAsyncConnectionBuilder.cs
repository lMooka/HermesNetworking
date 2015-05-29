using HermesNetworking.Networking.Packets.Handler;
using System.Net.Sockets;

namespace HermesNetworking.Networking.Connection
{
    public class MyAsyncConnectionBuilder : MyConnectionBuilder
    {
        public override IMyConnection Create(Socket socket, IMyPacketHandler handler)
        {
            return new MyAsyncConnection(socket, handler);
        }
    }
}
