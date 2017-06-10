using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerHUD2 : MonoBehaviour
{
    public NetworkManager manager;
    [SerializeField] public bool showGUI = true;
    [SerializeField] public int offsetX;
    [SerializeField] public int offsetY;
    [SerializeField] public int scale;

    // Runtime variable
    bool showServer = false;

    void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }

    void Update()
    {
        if (!showGUI)
            return;

        if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                manager.StartServer();
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                manager.StartHost();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                manager.StartClient();
            }
        }
        if (NetworkServer.active && NetworkClient.active)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                manager.StopHost();
            }
        }
    }

    void OnGUI()
    {
        if (!showGUI)
            return;

        int xpos = 10 + offsetX;
        int ypos = 40 + offsetY;
        int spacing = 24 * scale;

        if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
        {
            if (GUI.Button(new Rect(xpos, ypos, 200*scale, 20 * scale), "LAN Host(H)"))
            {
                manager.StartHost();
            }
            ypos += spacing;

            if (GUI.Button(new Rect(xpos, ypos, 105 * scale, 20 * scale), "LAN Client(C)"))
            {
                manager.StartClient();
            }
            manager.networkAddress = GUI.TextField(new Rect(xpos + 100 * scale, ypos, 95 * scale, 20 * scale), manager.networkAddress);
            ypos += spacing;

            if (GUI.Button(new Rect(xpos, ypos, 200 * scale, 20 * scale), "LAN Server Only(S)"))
            {
                manager.StartServer();
            }
            ypos += spacing;
        }
        else
        {
            if (NetworkServer.active)
            {
                GUI.Label(new Rect(xpos, ypos, 300 * scale, 20 * scale), "Server: port=" + manager.networkPort);
                ypos += spacing;
            }
            if (NetworkClient.active)
            {
                GUI.Label(new Rect(xpos, ypos, 300 * scale, 20 * scale), "Client: address=" + manager.networkAddress + " port=" + manager.networkPort);
                ypos += spacing;
            }
        }

        if (NetworkClient.active && !ClientScene.ready)
        {
            if (GUI.Button(new Rect(xpos, ypos, 200 * scale, 20 * scale), "Client Ready"))
            {
                ClientScene.Ready(manager.client.connection);

                if (ClientScene.localPlayers.Count == 0)
                {
                    ClientScene.AddPlayer(0);
                }
            }
            ypos += spacing;
        }

        if (NetworkServer.active || NetworkClient.active)
        {
            if (GUI.Button(new Rect(xpos, ypos, 200 * scale, 20 * scale), "Stop (X)"))
            {
                manager.StopHost();
            }
            ypos += spacing;
        }

    }
}
