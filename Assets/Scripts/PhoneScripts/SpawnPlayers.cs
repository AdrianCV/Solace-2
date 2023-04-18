using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject Player;
    public GameObject _shield;
    public GameObject _dash;

    // Start is called before the first frame update
    void Awake()
    {
        string preName;
        if (PhotonNetwork.LocalPlayer.CustomProperties["Guardian"] != null)
        {
            preName = "Guardian";
            _dash.SetActive(true);
        }
        else
        {
            preName = "IceClown";
            _shield.SetActive(true);
        }

        // if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        // {

        var player = PhotonNetwork.Instantiate(preName, new Vector2(0, 0), Quaternion.identity);

    }
}