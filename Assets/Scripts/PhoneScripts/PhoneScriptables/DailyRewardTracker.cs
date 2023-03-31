using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/DailyRewardTracker")]

public class DailyRewardTracker : ScriptableObject
{
    public bool HasRecievedDailyReward;

    public float Coins;
}
