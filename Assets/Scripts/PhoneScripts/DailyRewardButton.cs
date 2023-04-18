using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardButton : MonoBehaviour
{
    [SerializeField] private MainManager _manager;

    [SerializeField] private GameObject rewardMenu;
    [SerializeField] private GameObject hasRecievedRewardMenu;

    private void Start()
    {
        _manager = GameObject.FindObjectOfType<MainManager>();
    }

    public void RecieveDailyReward()
    {
        if (_manager.HasRecievedDailyReward)
        {
            // hasRecievedRewardMenu.SetActive(true);
        }
        else
        {
            // rewardMenu.SetActive(true);
            _manager.HasRecievedDailyReward = true;
            _manager.SoftCoins += 100;
        }
    }
}
