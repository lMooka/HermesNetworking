using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HermesNetworking.Networking.Packets.Serialization
{
    public static class GenericSerializer
    {
        public static byte[] GetBinary(object obj)
        {
            MemoryStream ms = new MemoryStream();

            new BinaryFormatter().Serialize(ms, obj);
            return ms.ToArray();
        }

        public static T GetObject<T>(byte[] bytes)
        {
            using (MemoryStream memStream = new MemoryStream(bytes))
            {
                return (T)new BinaryFormatter().Deserialize(memStream);
            }
        }

        public static int GetByteLength(object obj)
        {
            MemoryStream ms = new MemoryStream();

            new BinaryFormatter().Serialize(ms, obj);
            return ms.ToArray().Length;
        }
    }
}
