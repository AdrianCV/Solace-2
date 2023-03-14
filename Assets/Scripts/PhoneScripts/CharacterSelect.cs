using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviourPunCallbacks
{
    public void SelectGuardian()
    {
        Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
        hash.Add("Guardian", PhotonNetwork.LocalPlayer.ActorNumber);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

        PhotonNetwork.LoadLevel("Room");
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Room");
    }

    public void SelectIceClown()
    {
        Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
        hash.Add("IceClown", PhotonNetwork.LocalPlayer.ActorNumber);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

        PhotonNetwork.LoadLevel("Room");
    }
}
