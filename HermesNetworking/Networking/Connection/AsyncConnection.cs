using System;
using System.Net.Sockets;
using HermesNetworking.Networking.Packets;
using HermesNetworking.Networking.Packets.Handler;

namespace HermesNetworking.Networking.Connection
{
    public class AsyncConnection : IDisposable, IConnection
    {
        public Socket ConnectionSocket { get; set; }
        public PacketHandler Handler { get; set; }

        protected byte[] buffer;

        public AsyncConnection(Socket socket, PacketHandler handler)
        {
            this.ConnectionSocket = socket;
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

                Handler.PacketReceived(buf);
                
                buffer = new byte[HermesConfig.PACKET_BUFFER_SIZE];
                rcvSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceivedCallback, rcvSocket);
            }
            catch (Exception e)
            {
            }
        }

        public void GetReady()
        {
            ConnectionSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceivedCallback, ConnectionSocket);
        }

        public void Dispose()
        {
            try
            {
                ConnectionSocket.Disconnect(false);
                ConnectionSocket.Dispose();
                this.Dispose();
            }
            catch (Exception)
            { }
        }

        public void Send(IPacket packet)
        {
            ConnectionSocket.Send(packet.GetBuffer());
        }


        public void Disconnect()
        {
            Dispose();
        }
    }
}
