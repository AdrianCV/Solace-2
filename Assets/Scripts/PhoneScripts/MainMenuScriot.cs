using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenuScriot : MonoBehaviour
{
    [SerializeField] private GameObject rankedSelect;
    [SerializeField] private GameObject defaultMenu;

    private void Start()
    {
        InputSystem.EnableDevice(LightSensor.current);
    }

    public void StartButton()
    {
        rankedSelect.SetActive(true);
        defaultMenu.SetActive(false);
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
