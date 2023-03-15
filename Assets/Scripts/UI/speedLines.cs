using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedLines : MonoBehaviour
{
    public GameObject speedObject;
    public float speedValue = 0f;

    public float hp;
    public float maxHp;

    // Start is called before the first frame update
    void Start()
    {
        maxHp = FindObjectOfType<damagePlayer>().damageTaken;
    }

    void Update()
    {
        hp = FindObjectOfType<damagePlayer>().damageTaken;
        speedValue = ((1 - (hp / maxHp)) / 2);

        speedObject.GetComponent<Renderer>().material.SetFloat("_Strength", speedValue);
    }
}
