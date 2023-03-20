using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class MainMenuScriot : MonoBehaviour
{
    [SerializeField] private GameObject _rankedSelect;
    [SerializeField] private GameObject _defaultMenu;
    [SerializeField] private TMP_Text _coins;
    public float Coins;

    private void Start()
    {
        InputSystem.EnableDevice(LightSensor.current);
    }

    private void Update()
    {
        _coins.text = "Coins: " + Coins;
        Coins = Mathf.Clamp(Coins, 0, Mathf.Infinity);
    }

    public void StartButton()
    {
        _rankedSelect.SetActive(true);
        _defaultMenu.SetActive(false);
    }

    public void RankedButtons()
    {
        SceneManager.LoadScene("Loading");
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(100, 10, 300, 100), "LightLevel: " + LightSensor.current?.lightLevel.ReadValue());
    }
}
