using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Manager : MonoBehaviour
{
    public string player;
    public Transform[] playerSpawnPosition;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        Transform spawnPoint = playerSpawnPosition[Random.Range(0,playerSpawnPosition.Length)];
        PhotonNetwork.Instantiate(player,spawnPoint.position,spawnPoint.rotation);
    }
}
