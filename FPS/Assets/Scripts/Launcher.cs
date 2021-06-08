using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Connect();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Trying to connect...");
        Join();
        base.OnConnectedToMaster();
    }

    void Connect()
    {
        PhotonNetwork.GameVersion = "0.0";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Connected!");
        StartGame();
        base.OnJoinedRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to Join Room :(");
        Create();
        base.OnJoinRandomFailed(returnCode, message);
    }

    void Create()
    {
        Debug.Log("Creating a Room...");
        PhotonNetwork.CreateRoom("");
    }

    void Join()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void StartGame()
    {
       if(PhotonNetwork.CurrentRoom.PlayerCount==1)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }
}
