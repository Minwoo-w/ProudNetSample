using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class PhotonNetworkManager : MonoBehaviourPunCallbacks
{
    private string networkState = string.Empty;

    // Runtime Temp
    public GameObject loginPanel = null;
    public Text debugText = null;
    private float deltaTime = 0.0f;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connect");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("OnConnectedToMaster");
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions { MaxPlayers = 4 }, null);
        Debug.Log("OnJoinLobby");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinRoom");
        StartGame();
    }

    private void Update()
    {
        string curNetworkState = PhotonNetwork.NetworkClientState.ToString();
        if (networkState != curNetworkState)
        {
            networkState = curNetworkState;
        }
    }

    private void StartGame()
    {
        loginPanel.SetActive(false);
        GameObject go = PhotonNetwork.Instantiate("Player_Photon", Vector3.zero, Quaternion.identity);
        if (go != null)
        {
            Player player = go.GetComponent<Player>();
            PhotonView view = go.GetComponent<PhotonView>();
            player.SetIsMine(true);
        }
    }
}
