using System;

namespace Server {
    class Program {
        static void Main(string[] args) {
            Console.Title = "MMORPG Server";
            Config.SetServerConfig(50, 26950, 4096);
            Server.Start();
            Console.ReadKey();
        }
    }
}