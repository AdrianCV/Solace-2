using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelect : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _betMenu;
    [SerializeField] private GameObject _characterSelect;
    [SerializeField] private TMP_InputField _betAmount;
    private MainManager _manager;

    private void Start()
    {
        Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
        hash.Clear();
        _manager = GameObject.FindObjectOfType<MainManager>();
    }


    public void SelectGuardian()
    {
        Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
        hash.Add("Guardian", PhotonNetwork.LocalPlayer.ActorNumber);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

        ConfirmCharacter();
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

        ConfirmCharacter();
    }

    void ConfirmCharacter()
    {
        _characterSelect.SetActive(false);
        _betMenu.SetActive(true);
    }

    public void ConfirmBet()
    {
        try
        {
            var tempBet = int.Parse(_betAmount.text);
            _manager.BetAmount = tempBet > _manager.SoftCoins ? _manager.SoftCoins : tempBet;
        }
        catch
        {
            _manager.BetAmount = 0;
        }
        PhotonNetwork.LoadLevel("Room");
    }
}
