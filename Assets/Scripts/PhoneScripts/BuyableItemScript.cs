using System;
using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyableItemScript : MonoBehaviour
{
    [SerializeField] private Button _buyButton;
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private GameObject _boughtSprite;
    [SerializeField] private bool _repeatable;
    // [SerializeField] private bool _skin, _character;
    [SerializeField] private MainMenuScriot _mainMenu;
    [SerializeField] private int _coinValue;
    [SerializeField] private int _cost;

    [SerializeField] private MainManager _manager;
    [SerializeField] private BoughtItems _item;

    // Start is called before the first frame update
    private void Awake()
    {
        _manager = GameObject.FindObjectOfType<MainManager>();

        if (_item != null)
        {
            _itemName.text = _item.Name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_item != null)
        {
            if (_manager.PurchasedItems.Contains(_item.Name))
            {
                _buyButton.interactable = false;
                _boughtSprite.SetActive(true);
            }
        }
    }

    public void BuyItem()
    {
        // _manager.saveManager.SaveGame();

        if (_repeatable)
        {
            return;
        }

        if (_mainMenu.Tracker.Coins >= _cost)
        {
            _buyButton.interactable = false;
            _boughtSprite.SetActive(true);
            _mainMenu.Tracker.Coins -= _cost;

            _manager.PurchasedItems.Add(_item.Name);
        }
    }

    public void BuyCoins()
    {
        _mainMenu.Tracker.Coins += _coinValue;
    }
}
