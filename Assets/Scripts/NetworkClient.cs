using UnityEngine;
using System.Collections;

public class NetworkClient : MonoBehaviour
{
    public string connectionIP = "127.0.0.1";
    public int connectionPort = 25001;
    public NetworkLogLevel logLevel = NetworkLogLevel.Full;

    public Transform playerPrefab;

    void Start()
    {
        Network.logLevel = logLevel;
    }

    void OnConnectedToServer()
    {
        Debug.Log("Connected to server");
        SpawnPlayer();
    }

    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        if (info == NetworkDisconnection.LostConnection)
        {
            Debug.Log("Lost connection to the server");
        } else
        {
            Debug.Log("Successfully diconnected from the server");
        }
    }

    void OnFailedToConnect(NetworkConnectionError error)
    {
        Debug.Log("Could not connect to server: " + error);
    }

    void OnGUI()
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            GUI.Label(new Rect(10, 10, 200, 20), "Status: Disconnected");
            if (GUI.Button(new Rect(10, 30, 120, 20), "Client Connect"))
            {
                Network.Connect(connectionIP, connectionPort);
            }
        } else
        {
            if (Network.peerType == NetworkPeerType.Connecting)
            {
                GUI.Label(new Rect(10, 10, 200, 20), "Status: Connecting");
            } else
            {
                GUI.Label(new Rect(10, 10, 300, 20), "Status: Connected as Client " + Network.player.ToString());
                if (GUI.Button(new Rect(10, 30, 120, 20), "Disconnect"))
                {
                    Network.Disconnect(200);
                }
                
            }
        }
    }

    void SpawnPlayer()
    {
        Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0);
    }
}
