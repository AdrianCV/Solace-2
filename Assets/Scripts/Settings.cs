using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject[] otherUIElements;
    [SerializeField] private GameObject closeButton;

    public void SettingsButton()
    {
        foreach (GameObject element in otherUIElements)
        {
            element.SetActive(false);
        }
        closeButton.SetActive(true);
    }

    public void CloseSettings()
    {
        foreach (GameObject element in otherUIElements)
        {
            element.SetActive(true);
        }
        closeButton.SetActive(false);
    }
}
