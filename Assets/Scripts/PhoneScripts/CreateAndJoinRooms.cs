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

    MainManager _manager;
    AudioSource _audioSource;

    private void Start()
    {
        _manager = GameObject.FindObjectOfType<MainManager>();
        _audioSource = _manager.GetComponent<AudioSource>();
    }

    public void CreateRoom()
    {
        _audioSource.Play();
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
        _audioSource.Play();
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public void MainMenu()
    {
        _audioSource.Play();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }
}