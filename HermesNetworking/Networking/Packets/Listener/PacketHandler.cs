using System;
using HermesNetworking.Networking.Packets.Parser;

/* Converts and delegates the action of packet. */
namespace HermesNetworking.Networking.Packets.Listener
{
    public class PacketHandler
    {
        public IPacketParser Parser { get; set; }

        public PacketHandler()
        { }

        public PacketHandler(IPacketParser parser)
        {
            this.Parser = parser;
        }

        public void PacketReceived(byte[] buf)
        {
            if (Parser == null)
                throw new Exception("Failed to parse the packet. Parser was not defined.");

            Parser.Parse(buf).MyAction.Action();
        }
    }
}
