using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardButton : MonoBehaviour
{
    [SerializeField] private StatTracker dailyRewardTracker;

    [SerializeField] private GameObject rewardMenu;
    [SerializeField] private GameObject hasRecievedRewardMenu;

    public void RecieveDailyReward()
    {
        if (dailyRewardTracker.HasRecievedDailyReward)
        {
            hasRecievedRewardMenu.SetActive(true);
        }
        else
        {
            rewardMenu.SetActive(true);
            dailyRewardTracker.HasRecievedDailyReward = true;
            dailyRewardTracker.Coins += 500;
        }
    }
}
