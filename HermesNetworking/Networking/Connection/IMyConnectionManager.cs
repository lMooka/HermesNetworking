using HermesNetworking.Networking.Packets.Handler;
using System.Net.Sockets;

namespace HermesNetworking.Networking.Connection
{
    public interface IMyConnectionManager
    {
        IMyPacketHandler MyPacketHandler { get; set; }
        void NotifyNewConnection(IMyConnection connection);
    }
}
