using System;
using System.Net.Sockets;
using HermesNetworking.Networking.Packets;
using HermesNetworking.Networking.Packets.Handler;


namespace HermesNetworking.Networking.Connection
{
    public interface IMyConnection
    {
        Socket MySocket { get; set; }
        IMyPacketHandler MyPacketHandler { get; set; }

        void WaitNextPacket();
        void Disconnect();
    }
}
