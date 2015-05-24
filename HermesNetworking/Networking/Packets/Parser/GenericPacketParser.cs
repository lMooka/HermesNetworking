using HermesNetworking.Networking.Packets.Data;
using HermesNetworking.Networking.Packets.Serialization;

namespace HermesNetworking.Networking.Packets.Parser
{
    public class GenericPacketParser : IPacketParser
    {
        public PacketData Parse(byte[] buf)
        {
            return GenericSerializer.GetObject<GenericPacketData>(buf);
        }
    }
}
