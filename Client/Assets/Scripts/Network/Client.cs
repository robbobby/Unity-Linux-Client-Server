using System.Net.Sockets;
using UnityEditor.MemoryProfiler;
using UnityEngine;

namespace Network {
    public partial class Client : MonoBehaviour {
        public static Client instance;
        public int MyId { get; }
        public TCP tcp;

        private void Awake() {
            if (instance == null) {
                instance = this;
            } else if (instance != this) {
                Debug.Log($"instance of {instance.GetType()} already exists, destroying instance");
                Destroy(this);
            }
        }

        private void Start() { tcp = new TCP(); }

        public void ConnectToServer() {
            tcp.Connect();
        }
    }
}