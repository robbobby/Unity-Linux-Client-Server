using System;

namespace Server {
    public static class Config {
        public static int MaxPlayers { get; private set; }
        public static int Port { get; private set; }
        public static int BufferSize { get; private set; }

        public static void SetServerConfig(int maxPlayers, int port, int bufferSize) {
            if(MaxPlayers == 0)
                MaxPlayers = maxPlayers;
            else
                ErrorChangingConfigWhenServerRunning();
            if(Port == 0)
                Port = port;
            else
                ErrorChangingConfigWhenServerRunning();
            if (BufferSize == 0) 
                BufferSize = bufferSize;
            else
                ErrorChangingConfigWhenServerRunning();
        }

        private static void ErrorChangingConfigWhenServerRunning() {
            Console.WriteLine("Cannot change Server config while server is running\n" +
                              "Please set the initial config in program.cs");
        }
    }
}