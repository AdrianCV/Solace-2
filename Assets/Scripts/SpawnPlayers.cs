using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(Player.name, new Vector2(0, 0), Quaternion.identity);
    }
}