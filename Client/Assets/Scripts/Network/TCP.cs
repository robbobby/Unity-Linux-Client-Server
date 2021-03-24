using System;
using System.Net.Sockets;

namespace Network {
    public partial class Client {
        public class TCP {
            public TcpClient socket;
            private NetworkStream stream;
            private byte[] receiveBuffer;

            public void Connect() {
                socket = new TcpClient() {
                    ReceiveBufferSize = Config.BufferSize,
                    SendBufferSize = Config.BufferSize};
                
                receiveBuffer = new byte[Config.BufferSize];
                socket.BeginConnect(Config.Ip, Config.Port, ConnectCallback, null);
            }

            private void ConnectCallback(IAsyncResult result) {
                socket.EndConnect(result);
                if (!socket.Connected) return;
                stream = socket.GetStream();
                stream.BeginRead(receiveBuffer, 0, Config.BufferSize, ReceiveCallback, null);
            }

            private void ReceiveCallback(IAsyncResult result) {
                try {
                    int byteLength = stream.EndRead(result);
                    if (byteLength <= 0) return;
                    byte[] data = new byte[byteLength];
                    Array.Copy(receiveBuffer, data, byteLength);
                    // TODO: Handle data

                    stream.BeginRead(receiveBuffer, 0, Config.BufferSize, ReceiveCallback, null);
                } catch (Exception e) {
                    Console.WriteLine($"Error receiving {e}");
                    // TODO: Disconnect client
                }
            }
        }
    }
}