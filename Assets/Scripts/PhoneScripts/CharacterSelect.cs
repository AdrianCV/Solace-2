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
    [SerializeField] private TMP_Text _betField;
    [SerializeField] private TMP_Text _softCurrency;
    [SerializeField] private TMP_Text _hardCurrency;

    [SerializeField] private GameObject _selectedGuardian;
    [SerializeField] private GameObject _selectedIceClown;

    int _betAmount;

    private MainManager _manager;
    AudioSource _audioSource;

    private void Start()
    {
        SelectGuardian();
        _manager = GameObject.FindObjectOfType<MainManager>();
        _audioSource = _manager.GetComponent<AudioSource>();
    }

    private void Update()
    {
        _betField.text = "" + _betAmount;
        _softCurrency.text = "" + _manager.SoftCoins;
        _hardCurrency.text = "" + _manager.Coins;
    }


    public void SelectGuardian()
    {
        Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
        hash.Clear();
        hash.Add("Guardian", PhotonNetwork.LocalPlayer.ActorNumber);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        _audioSource.Play();

        _selectedGuardian.SetActive(true);
        _selectedIceClown.SetActive(false);
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Room");
    }

    public void SelectIceClown()
    {
        Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
        hash.Clear();
        hash.Add("IceClown", PhotonNetwork.LocalPlayer.ActorNumber);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        _audioSource.Play();

        _selectedGuardian.SetActive(false);
        _selectedIceClown.SetActive(true);
    }

    public void ConfirmCharacter()
    {
        _characterSelect.SetActive(false);
        _betMenu.SetActive(true);
        _audioSource.Play();
    }

    public void IncreaseBet()
    {
        _betAmount += 10;
        _audioSource.Play();
    }

    public void DecreaseBet()
    {
        _betAmount -= 10;
        _audioSource.Play();
    }

    public void ConfirmBet()
    {
        _audioSource.Play();
        try
        {
            var tempBet = int.Parse(_betField.text);
            _manager.BetAmount = tempBet > _manager.SoftCoins ? _manager.SoftCoins : tempBet;
        }
        catch
        {
            _manager.BetAmount = 0;
        }
        PhotonNetwork.LoadLevel("Room");
    }
}
