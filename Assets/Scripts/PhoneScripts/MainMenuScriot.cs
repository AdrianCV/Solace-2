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
    AudioSource _audioSource;
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
        _audioSource = Tracker.GetComponent<AudioSource>();

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
        _audioSource.Play();
        SceneManager.LoadScene("CharacterSelect");
    }

    public void RankedButtons()
    {
        _audioSource.Play();
        gameObject.SetActive(false);
        SceneManager.LoadScene("CharacterSelect");
    }

    public void Return()
    {
        if (_rankedSelect != null)
        {
            _rankedSelect.SetActive(false);
            _defaultMenu.SetActive(true);
            _audioSource.Play();
        }
        else
        {
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("MainMenu");
            _audioSource.Play();
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(100, 10, 300, 100), "LightLevel: " + LightSensor.current?.lightLevel.ReadValue());
    }

    public void ShowDailyTask()
    {
        _dailyTask.SetActive(true);
        _audioSource.Play();
    }

    public void ShowDailyReward()
    {
        _dailyReward.SetActive(true);
        _audioSource.Play();
    }

    public void CloseDailyMenu()
    {
        _dailyReward.SetActive(false);
        _dailyTask.SetActive(false);
        _audioSource.Play();
    }
}
