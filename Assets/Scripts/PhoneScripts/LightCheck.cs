using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using TMPro;

public class LightCheck : MonoBehaviour
{
    [SerializeField] private GameObject _darkBackground;
    [SerializeField] private GameObject _lightBackground;


    [SerializeField] private GameObject _damageUI;
    [SerializeField] private GameObject _ui;
    [SerializeField] private GameObject _loadingScreen;
    private TMP_Text _loadingText;



    // Start is called before the first frame update
    void Start()
    {
        _damageUI.SetActive(false);
        _loadingText = _loadingScreen.GetComponentInChildren<TMP_Text>();

        StartCoroutine(LoadingScreen());

        InputSystem.EnableDevice(LightSensor.current);


        if (LightSensor.current?.lightLevel.ReadValue() < 200)
        {
            _darkBackground.SetActive(true);
        }
        else
        {
            _lightBackground.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            StopCoroutine(LoadingScreen());
            StartCoroutine(StartGame());
        }
    }

    IEnumerator LoadingScreen()
    {
        _loadingText.text = "Waiting For Players";
        yield return new WaitForSeconds(1);
        _loadingText.text = "Waiting For Players.";
        yield return new WaitForSeconds(1);
        _loadingText.text = "Waiting For Players..";
        yield return new WaitForSeconds(1);
        _loadingText.text = "Waiting For Players...";
        yield return new WaitForSeconds(1);
        StartCoroutine(LoadingScreen());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);
        _loadingScreen.SetActive(false);
        _damageUI.SetActive(true);
        _ui.SetActive(true);
    }
}
