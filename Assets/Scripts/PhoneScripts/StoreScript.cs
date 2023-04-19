using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScript : MonoBehaviour
{
    [SerializeField] private GameObject defaultMenu;
    [SerializeField] private GameObject _mainStore;
    [SerializeField] private GameObject storeMenu;
    [SerializeField] private GameObject skinMenu;
    [SerializeField] private GameObject characterMenu;
    [SerializeField] private GameObject coinsMenu;

    [SerializeField] private GameObject _buttons;
    [SerializeField] private GameObject _backButton;
    [SerializeField] private GameObject _mainBackButton;

    [SerializeField] private MainManager _manager;
    [SerializeField] AudioSource _audioSource;

    private void Start()
    {
        _manager = GameObject.FindObjectOfType<MainManager>();
        _audioSource = _manager.GetComponent<AudioSource>();
    }

    public void ShowStoreMenu()
    {
        _audioSource.Play();
        defaultMenu.SetActive(false);
        _mainStore.SetActive(true);
    }

    public void CloseStore()
    {
        _audioSource.Play();
        defaultMenu.SetActive(true);
        _mainStore.SetActive(false);
    }

    public void ShowSkins()
    {
        _audioSource.Play();
        skinMenu.SetActive(true);
        storeMenu.SetActive(false);
        characterMenu.SetActive(false);
        coinsMenu.SetActive(false);
        _buttons.SetActive(false);
        _mainBackButton.SetActive(false);
        _backButton.SetActive(true);
    }

    public void CharacterMenu()
    {
        _audioSource.Play();
        skinMenu.SetActive(false);
        storeMenu.SetActive(false);
        characterMenu.SetActive(true);
        coinsMenu.SetActive(false);
        _buttons.SetActive(false);
        _mainBackButton.SetActive(false);
        _backButton.SetActive(true);
    }

    public void CoinsMenu()
    {
        _audioSource.Play();
        skinMenu.SetActive(false);
        storeMenu.SetActive(false);
        characterMenu.SetActive(false);
        coinsMenu.SetActive(true);
        _buttons.SetActive(false);
        _mainBackButton.SetActive(false);
        _backButton.SetActive(true);
    }

    public void CloseSmallMenu()
    {
        _audioSource.Play();
        _backButton.SetActive(false);
        _mainBackButton.SetActive(true);
        storeMenu.SetActive(true);
        _buttons.SetActive(true);
        skinMenu.SetActive(false);
        characterMenu.SetActive(false);
        coinsMenu.SetActive(false);
    }
}
