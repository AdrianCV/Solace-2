using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour, IDataPersistance
{

    public int SoftCurrency;
    public int HardCurrency;

    public List<string> PurchasedItems;


    [SerializeField] private TextMeshProUGUI _softText;
    [SerializeField] private TextMeshProUGUI _hardText;

    public void LoadData(GameData data)
    {
        SoftCurrency = data.softCurrency;
        HardCurrency = data.hardCurrency;
        PurchasedItems = data.purchasedItems;
    }

    public void SaveData(ref GameData data)
    {
        data.softCurrency = SoftCurrency;
        data.hardCurrency = HardCurrency;
        data.purchasedItems = PurchasedItems;
    }

    private void Update()
    {
        _softText.text = "S: " + SoftCurrency.ToString();
        _hardText.text = "H: " + HardCurrency.ToString();
    }

}