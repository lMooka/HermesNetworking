using System;
using HermesNetworking.Networking.Packets.Serialization;
using HermesNetworking.Networking.Packets.Data;

namespace HermesNetworking.Networking.Packets
{
    [Serializable()]
    public class GenericPacket : IPacket
    {
        protected PacketData Data;

        public GenericPacket(PacketData data)
        {
            this.Data = data;
        }

        public int GetId()
        {
            return this.Data.Id;
        }

        public int GetSize()
        {
            return GenericSerializer.GetByteLength(this.Data);
        }

        public byte[] GetBuffer()
        {
            return GenericSerializer.GetBinary(this.Data);
        }
    }
}
