using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HermesNetworking.Networking.Packets
{
    public interface IPacket
    {
        int GetId();
        int GetSize();
        byte[] GetBuffer();
    }
}
