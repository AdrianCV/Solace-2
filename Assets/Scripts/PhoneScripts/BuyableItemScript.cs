using System;
using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyableItemScript : MonoBehaviour
{
    [SerializeField] private Button _buyButton;
    [SerializeField] private GameObject _boughtSprite;
    [SerializeField] private bool _repeatable;
    [SerializeField] private bool _skin, _character;
    [SerializeField] private MainMenuScriot _mainMenu;
    [SerializeField] private int _coinValue;
    [SerializeField] private int _cost;

    [SerializeField] private MainManager _manager;
    [SerializeField] private BoughtItems _item;

    // Start is called before the first frame update
    private void Awake()
    {
        _manager = GameObject.FindObjectOfType<MainManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_skin)
        {
            if (_manager.BoughtSkins.Contains(_item))
            {
                _buyButton.interactable = false;
                _boughtSprite.SetActive(true);
            }
            else
            {
                _buyButton.interactable = true;
                _boughtSprite.SetActive(false);
            }
        }
        else if (_character)
        {
            if (_manager.BoughtCharacters.Contains(_item))
            {
                _buyButton.interactable = false;
                _boughtSprite.SetActive(true);
            }
            else
            {
                _buyButton.interactable = true;
                _boughtSprite.SetActive(false);
            }
        }
    }

    public void BuyItem()
    {
        if (_repeatable)
        {
            return;
        }

        if (_mainMenu.Tracker.Coins >= _cost)
        {
            _buyButton.interactable = false;
            _boughtSprite.SetActive(true);
            _mainMenu.Tracker.Coins -= _cost;

            if (_skin)
            {
                _manager.BoughtSkins.Add(_item);
            }
            else if (_character)
            {
                _manager.BoughtCharacters.Add(_item);
            }
        }
    }

    public void BuyCoins()
    {
        _mainMenu.Tracker.Coins += _coinValue;
    }
}
