using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HermesNetworking.Networking.Packets.Serialization
{
    public class MyGenericSerializer : IMyPacketSerializer
    {
        public byte[] Serialize(IMyPacket packet)
        {
            return GetBinary(packet);
        }

        public IMyPacket Deserialize(byte[] buf)
        {
            return GetObject<IMyPacket>(buf);
        }

        protected byte[] GetBinary(object obj)
        {
            MemoryStream ms = new MemoryStream();

            new BinaryFormatter().Serialize(ms, obj);
            return ms.ToArray();
        }

        protected T GetObject<T>(byte[] bytes)
        {
            using (MemoryStream memStream = new MemoryStream(bytes))
            {
                return (T)new BinaryFormatter().Deserialize(memStream);
            }
        }

        protected int GetByteLength(object obj)
        {
            MemoryStream ms = new MemoryStream();

            new BinaryFormatter().Serialize(ms, obj);
            return ms.ToArray().Length;
        }
    }
}
