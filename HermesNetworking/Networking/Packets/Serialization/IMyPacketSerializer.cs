using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HermesNetworking.Networking.Packets.Serialization
{
    public interface IMyPacketSerializer
    {
        byte[] Serialize(IMyPacket packet);
        IMyPacket Deserialize(byte[] buf);
    }
}
