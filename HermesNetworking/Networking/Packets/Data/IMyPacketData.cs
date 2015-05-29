using HermesNetworking.Networking.Action;

namespace HermesNetworking.Networking.Packets.Data
{
    public interface IMyPacketData
    {
        byte[] GetByteData();
        IMyPacketAction GetMyAction();
    }
}
