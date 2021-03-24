using UnityEngine;

namespace Network {
    public static class Config {
        public static string Ip { get; private set; } = "127.0.0.1";
        public static int Port { get; private set; } = 26950;
        public static int BufferSize { get; private set; } = 4096;

        public static void SetServerConfig(string ip, int port, int bufferSize) {
            if (Ip == "")
                Ip = ip;
            else
                ErrorChangingConfigWhenServerRunning();
            if (Port == 0)
                Port = port;
            else
                ErrorChangingConfigWhenServerRunning();
            if (BufferSize == 0)
                BufferSize = bufferSize;
            else
                ErrorChangingConfigWhenServerRunning();
        }

        private static void ErrorChangingConfigWhenServerRunning() {
            Debug.Log("Cannot change Server config while server is running\n" +
                      "Please set the initial config in program.cs");
        }
    }
}
