using scripts.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionScript : MonoBehaviour
{
    [SerializeField] TMP_InputField ipInput;
    [SerializeField] Button connectButton;
    [SerializeField] TMP_Text connectButtonTmp;
    [SerializeField] string connectLabel = "Connect", disconnectLabel = "Disconnect";
    [SerializeField] DeviceClient deviceClient;
    IClientSocket socket => deviceClient.ClientSocket;
    private void Awake()
    {
        connectButton.onClick.AddListener(OnClick);
        socket.Disconnected += OnDisconnect;
        socket.Connected += OnConnect;
    }

    public void OnClick()
    {
        if (!IPAddress.TryParse(ipInput.text, out var ip))
            return;
        Debug.Log("Clicked");
        connectButton.interactable = false;
        if (!socket.IsConnected)
        {
            var ipString = ip.ToString();
            if (ipString == "127.0.0.1")
                ipString = "localhost";
            deviceClient.Connect(ipString);
            Debug.Log("Attempting to connect...");
        }
        else
        {
            deviceClient.Disconnect();
        }
    }
    public void OnConnect()
    {
        connectButtonTmp.text = disconnectLabel;
        connectButton.interactable = true;
    }
    public void OnDisconnect()
    {
        connectButtonTmp.text = connectLabel;
        connectButton.interactable = true;
    }
}
