using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour, IDataPersistance
{
    public static MainManager Instance;

    public int Coins;

    public float RankedPoint;

    public float RankConstant;

    public int BetAmount;

    public List<string> PurchasedItems;

    public DataPersistanceManager saveManager;

    public void LoadData(GameData data)
    {
        // SoftCurrency = data.softCurrency;
        Coins = data.hardCurrency;
        PurchasedItems = data.purchasedItems;
    }

    public void SaveData(ref GameData data)
    {
        // data.softCurrency = SoftCurrency;
        data.hardCurrency = Coins;
        data.purchasedItems = PurchasedItems;
    }

    private void Awake()
    {
        // saveManager = GameObject.FindObjectOfType<DataPersistanceManager>();
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
