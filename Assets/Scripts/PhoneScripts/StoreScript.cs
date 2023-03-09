using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScript : MonoBehaviour
{
    [SerializeField] private GameObject defaultMenu;
    [SerializeField] private GameObject storeMenu;
    [SerializeField] private GameObject skinMenu;
    [SerializeField] private GameObject characterMenu;
    [SerializeField] private GameObject coinsMenu;

    public void ShowStoreMenu()
    {
        defaultMenu.SetActive(false);
        storeMenu.SetActive(true);
    }

    public void CloseStore()
    {
        defaultMenu.SetActive(true);
        storeMenu.SetActive(false);
    }

    public void ShowSkins()
    {
        skinMenu.SetActive(true);
        characterMenu.SetActive(false);
        coinsMenu.SetActive(false);
    }

    public void CharacterMenu()
    {
        skinMenu.SetActive(false);
        characterMenu.SetActive(true);
        coinsMenu.SetActive(false);
    }

    public void CoinsMenu()
    {
        skinMenu.SetActive(false);
        characterMenu.SetActive(false);
        coinsMenu.SetActive(true);
    }
}
