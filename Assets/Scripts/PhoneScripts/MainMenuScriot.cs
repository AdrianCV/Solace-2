using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using Photon.Pun;


public class MainMenuScriot : MonoBehaviour
{
    [SerializeField] private GameObject _rankedSelect;
    [SerializeField] private GameObject _defaultMenu;
    [SerializeField] private TMP_Text _coins;
    public StatTracker Tracker;

    private void Start()
    {
        InputSystem.EnableDevice(LightSensor.current);
    }

    private void Update()
    {
        if (_coins == null)
        {
            return;
        }

        _coins.text = "Coins: " + Tracker.Coins;
        Tracker.Coins = Mathf.Clamp(Tracker.Coins, 0, Mathf.Infinity);
    }

    public void StartButton()
    {
        _rankedSelect.SetActive(true);
        _defaultMenu.SetActive(false);
    }

    public void RankedButtons()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("Loading");
    }

    public void Return()
    {
        if (_rankedSelect != null)
        {
            _rankedSelect.SetActive(false);
            _defaultMenu.SetActive(true);
        }
        else
        {
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(100, 10, 300, 100), "LightLevel: " + LightSensor.current?.lightLevel.ReadValue());
    }
}
