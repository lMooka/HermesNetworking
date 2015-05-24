using System;
using System.Net.Sockets;
using HermesNetworking.Networking.Packets;
using HermesNetworking.Networking.Packets.Handler;


namespace HermesNetworking.Networking.Connection
{
    public interface IConnection
    {
        Socket ConnectionSocket { get; set; }
        PacketHandler Handler { get; set; }

        void GetReady();
        void Disconnect();
    }
}
