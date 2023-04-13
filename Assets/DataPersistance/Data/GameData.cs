using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]

public class GameData
{
    public int softCurrency;
    public int hardCurrency;
    public DateTime lastDailyReward;
    public DateTime lastDailyChallenge;

    public GameData()
    {
        this.softCurrency = 0;
        this.hardCurrency = 0;
    }

}
