using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class UIDamage : MonoBehaviour
{
    public damagePlayer Character;

    [SerializeField] GameObject[] _hearts;
    // Start is called before the first frame update
    void Start()
    {
        // print(PhotonNetwork.LocalPlayer.ActorNumber);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _hearts.Length; i++)
        {
            if (Character.lives < i + 1)
            {
                _hearts[i].SetActive(true);
            }
        }
    }
}
