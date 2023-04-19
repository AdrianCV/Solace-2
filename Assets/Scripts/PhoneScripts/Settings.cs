using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject _defaultMenu;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject _currency;
    [SerializeField] private GameObject _parent;
    // [SerializeField] private GameObject resetButton;
    [SerializeField] private MainManager _stats;
    [SerializeField] AudioSource _audioSource;

    private void Awake()
    {
        _stats = GameObject.FindObjectOfType<MainManager>();
        _audioSource = _stats.GetComponent<AudioSource>();
    }

    public void SettingsButton()
    {
        _audioSource.Play();
        _defaultMenu.SetActive(false);
        closeButton.SetActive(true);
        _parent.SetActive(true);
        _currency.SetActive(false);

        // resetButton.SetActive(true);
    }

    public void Reset()
    {
        _stats.PurchasedItems.Clear();
        _stats.Coins = 0;
        // _stats.saveManager.SaveGame();
    }

    public void CloseSettings()
    {
        _audioSource.Play();
        _defaultMenu.SetActive(true);
        closeButton.SetActive(false);
        _parent.SetActive(false);
        _currency.SetActive(true);

        // resetButton.SetActive(false);
    }
}
