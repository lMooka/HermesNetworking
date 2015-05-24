using System;
using System.Net.Sockets;
using HermesNetworking.Networking.Packets;
using HermesNetworking.Networking.Packets.Handler;


namespace HermesNetworking.Networking.Connection
{
    interface IConnection
    {
        int ConnectionId { get; set; }
        Socket ConnectionSocket { get; set; }
        PacketHandler Handler { get; set; }
    }
}
