using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Network {
    public class UiManager : MonoBehaviour {
        public static UiManager Instance { get; set; }
        public GameObject StartMenu;
        public TMP_InputField UserNameField;
        public TMP_InputField UserPasswordField;
        

        public void Awake() {
            if (Instance == null)
                Instance = this;
            else if (Instance != this) {
                Debug.Log($"instance of {Instance.GetType()} already exists, destroying instance");
                Destroy(Instance);
            }
        }

        public void ConnectToServer() {
            StartMenu.SetActive(false);
            UserNameField.interactable = false;
            // UserPasswordField.interactable = false;
            Client.instance.ConnectToServer();
        }
    }
}
