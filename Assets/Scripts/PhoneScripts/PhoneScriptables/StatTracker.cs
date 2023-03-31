using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/StatTracker")]

public class StatTracker : ScriptableObject
{
    public bool HasRecievedDailyReward;

    public float Coins;

    public float RankedPoint;

    public float OpponentRankedPoints;

    public float RankConstant;
}
