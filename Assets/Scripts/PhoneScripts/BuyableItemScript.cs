using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyableItemScript : MonoBehaviour
{
    [SerializeField] private Button _buyButton;
    [SerializeField] private GameObject _boughtSprite;
    [SerializeField] private bool _repeatable;
    [SerializeField] private MainMenuScriot _mainMenu;
    [SerializeField] private int _coinValue;
    [SerializeField] private int _cost;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuyItem()
    {
        if (!_repeatable)
        {
            if (_mainMenu.Coins >= _cost)
            {
                _buyButton.interactable = false;
                _boughtSprite.SetActive(true);
                _mainMenu.Coins -= _cost;
            }
        }
    }

    public void BuyCoins()
    {
        _mainMenu.Coins += _coinValue;
    }
}
