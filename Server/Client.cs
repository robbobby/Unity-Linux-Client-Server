using System;
using System.Net.Sockets;

namespace Server {
    public class Client {
        public int Id { get; }
        public TcpConnection TcpConnection { get; }
        public Client(int id) {
            Id = id;
            TcpConnection = new TcpConnection(id);
        }
    }

    public class TcpConnection {
        public bool IsConnected { get; private set; }
        public TcpClient Socket { get; set; }
        private readonly int id;
        private NetworkStream networkStream;
        private byte[] receiveBuffer;
        
        public TcpConnection(int id) => this.id = id;

        public void Connect(TcpClient socket) {
            this.Socket = socket;
            socket.ReceiveBufferSize = Config.BufferSize;
            socket.SendBufferSize = Config.BufferSize;
            receiveBuffer = new byte[Config.BufferSize];
            
            networkStream = socket.GetStream();
            networkStream.BeginRead(receiveBuffer, 0, Config.BufferSize, ReceiveCallback, null);
            IsConnected = true;
            // TODO: Respond to client
        }


        private void ReceiveCallback(IAsyncResult result) {
            try {
                int byteLength = networkStream.EndRead(result);
                if(byteLength <= 0) return;
                byte[] data = new byte[byteLength];
                Array.Copy(receiveBuffer, data, byteLength);
                // TODO: Handle data
                
                networkStream.BeginRead(receiveBuffer, 0, Config.BufferSize, ReceiveCallback, null);
            } catch (Exception e) {
                Console.WriteLine($"Error receiving {e}");
                // TODO: Disconnect client
            }
        }
    }
}