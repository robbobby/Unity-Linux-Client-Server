using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Net;
using System.Net.Sockets;

namespace Server {
    public class Server {
        public static Dictionary<int, Client> Clients = new Dictionary<int, Client>();
        private static TcpListener tcpListener;
        public static void Start() {
            Console.WriteLine($"Starting server...");
            InitialiseClientDictionary();
            tcpListener = new TcpListener(IPAddress.Any, Config.Port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TcpConnectCallback), null);
            Console.WriteLine($"Server started on port: {Config.Port}");
        }

        private static void TcpConnectCallback(IAsyncResult result) {
            TcpClient client = tcpListener.EndAcceptTcpClient(result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TcpConnectCallback), null); // continues listening for connections
            Console.WriteLine($"Incoming connection from {client.Client.RemoteEndPoint}...");
            for (int i = 1; i <= Config.MaxPlayers; i++) {
                if (!Clients[i].TcpConnection.IsConnected) {
                    Clients[i].TcpConnection.Connect(client);
                    Console.WriteLine($"Client connected at position {i}");
                    return;
                }
            }
            Console.WriteLine($"{client.Client.RemoteEndPoint} failed to connect, Server is full");
        }

        private static void InitialiseClientDictionary() {
            for (int i = 1; i <= Config.MaxPlayers; i++) {
                Clients.Add(i, new Client(i));
            }
        }
    }
}