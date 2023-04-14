using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject[] otherUIElements;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject resetButton;
    [SerializeField] private MainManager _stats;

    private void Awake()
    {
        _stats = GameObject.FindObjectOfType<MainManager>();
    }

    public void SettingsButton()
    {
        foreach (GameObject element in otherUIElements)
        {
            element.SetActive(false);
        }
        closeButton.SetActive(true);
        resetButton.SetActive(true);
    }

    public void Reset()
    {
        _stats.PurchasedItems.Clear();
        _stats.Coins = 0;
        // _stats.saveManager.SaveGame();
    }

    public void CloseSettings()
    {
        foreach (GameObject element in otherUIElements)
        {
            element.SetActive(true);
        }
        closeButton.SetActive(false);
        resetButton.SetActive(false);
    }
}
