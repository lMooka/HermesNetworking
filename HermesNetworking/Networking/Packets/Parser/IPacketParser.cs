using HermesNetworking.Networking.Packets.Data;
using System;

namespace HermesNetworking.Networking.Packets.Parser
{
    public interface IPacketParser
    {
        PacketData Parse(byte[] buf);
    }
}
