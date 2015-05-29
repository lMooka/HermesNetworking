using System;

/* Converts and delegates the action of packet. */
namespace HermesNetworking.Networking.Packets.Handler
{
    public interface IMyPacketHandler
    {
        void Handle(IMyPacket packet);
        void Handle(byte[] buf);
    }
}
