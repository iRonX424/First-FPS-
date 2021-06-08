using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Manager : MonoBehaviour
{
    public string player;
    public Transform playerSpawnPosition;

    private void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        PhotonNetwork.Instantiate(player,playerSpawnPosition.position,playerSpawnPosition.rotation);
    }
}
