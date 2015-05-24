using System;
using HermesNetworking.Networking.Action;

namespace HermesNetworking.Networking.Packets.Data
{
    [Serializable()]
    public abstract class PacketData
    {
        public int Id;
        public IPacketAction MyAction;
    }
}
