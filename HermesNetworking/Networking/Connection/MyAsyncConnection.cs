using System;
using System.Net.Sockets;
using HermesNetworking.Networking.Packets;
using HermesNetworking.Networking.Packets.Handler;
using HermesNetworking.Networking.Packets.Serialization;

namespace HermesNetworking.Networking.Connection
{
    public class MyAsyncConnection : IMyConnection
    {
        public Socket MySocket { get; set; }
        public IMyPacketSerializer MyPacketSerializer { get; set; }
        public IMyPacketHandler MyPacketHandler { get; set; }

        protected byte[] buffer;

        public MyAsyncConnection(Socket socket, IMyPacketHandler handler)
        {
            this.MySocket = socket;
            buffer = new byte[HermesConfig.PACKET_BUFFER_SIZE];
        }

        protected virtual void ReceivedCallback(IAsyncResult result)
        {
            try
            {
                Socket rcvSocket = result.AsyncState as Socket;
                int bufSize = rcvSocket.EndReceive(result);

                byte[] buf = new byte[bufSize];
                Buffer.BlockCopy(buffer, 0, buf, 0, bufSize);

                MyPacketSerializer.Deserialize(buf);
                
                buffer = new byte[HermesConfig.PACKET_BUFFER_SIZE];
                rcvSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceivedCallback, rcvSocket);
            }
            catch (Exception e)
            {
            }
        }

        public void WaitNextPacket()
        {
            MySocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceivedCallback, MySocket);
        }

        public void Send(IMyPacket packet)
        {
            MySocket.Send(packet.GetBuffer());
        }

        public void Disconnect()
        {
            MySocket.Disconnect(false);
        }
    }
}
