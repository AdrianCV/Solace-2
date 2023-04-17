using System.Threading.Tasks.Sources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    [SerializeField] bool _twoPlayer;

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();

        if (_twoPlayer)
        {
            roomOptions.MaxPlayers = 2;
        }
        else
        {
            roomOptions.MaxPlayers = 1;
        }

        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public void MainMenu()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }
}