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
    [SerializeField] private TMP_Text _softCoins;
    [SerializeField] private bool onPhone;
    [SerializeField] private GameObject _dailyReward;
    [SerializeField] private GameObject _dailyTask;
    public MainManager Tracker;

    private void Awake()
    {
        Tracker = GameObject.FindObjectOfType<MainManager>();

        if (onPhone)
        {
            InputSystem.EnableDevice(LightSensor.current);
        }
    }

    private void Update()
    {
        if (_coins == null)
        {
            return;
        }

        _coins.text = "" + Tracker.Coins;
        _softCoins.text = "" + Tracker.SoftCoins;
        // print(_coins.text);
        Tracker.Coins = Mathf.Max(Tracker.Coins, 0);
    }

    public void StartButton()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void RankedButtons()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("CharacterSelect");
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

    public void ShowDailyTask()
    {
        _dailyTask.SetActive(true);
    }

    public void ShowDailyReward()
    {
        _dailyReward.SetActive(true);
    }

    public void CloseDailyMenu()
    {
        _dailyReward.SetActive(false);
        _dailyTask.SetActive(false);
    }
}
