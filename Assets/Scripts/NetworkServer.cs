using UnityEngine;
using System.Collections;

public class NetworkServer : MonoBehaviour
{
    public int listeningPort = 25001;
    public int maxConnections = 1024;
    public NetworkLogLevel logLevel = NetworkLogLevel.Full;
    public bool secure = false;
    public bool useNat = false;
    public string password = "";

    public Transform playerPrefab;

    void Start()
    {
        Network.logLevel = logLevel;

        Network.natFacilitatorIP = "0.0.0.0";
        Network.natFacilitatorPort = 0;
        MasterServer.dedicatedServer = true; // useless ?
        MasterServer.ipAddress = "0.0.0.0";
        MasterServer.port = 0;

        LaunchServer();
    }

    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        Debug.Log("Local server connection disconnected");
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("Player " + player + " connected from " + player.ipAddress + ":" + player.port);
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("Clean up after player " + player);
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }

    void OnServerInitialized()
    {
        Debug.Log("Server initialized and ready on port " + listeningPort);
    }

    void LaunchServer()
    {
        Network.incomingPassword = password;
        if (secure)
        {
            Network.InitializeSecurity();
        }
        Network.InitializeServer(maxConnections, listeningPort, useNat);
    }
}
