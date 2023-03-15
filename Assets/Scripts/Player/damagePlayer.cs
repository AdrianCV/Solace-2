using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using Photon.Pun;

public class damagePlayer : MonoBehaviour
{
    public float damageTaken = 0;
    public float maxHp;
    private Rigidbody2D rb;
    public GameObject hitEffect;

    public Animator deathAnim;
    public bool isDead = false;
    public bool isMortal = true;
    public bool shieldOn = false;
    private bool enterIn = false;
    private bool instantDeath = false;

    private dashMove dashScript;


    private character _character;

    [SerializeField] private GameObject _damageBox;

    // Start is called before the first frame update
    void Start()
    {
        maxHp = damageTaken;
        rb = this.GetComponent<Rigidbody2D>();
        dashScript = FindObjectOfType<dashMove>();

        _character = GetComponent<character>();

        // _damageBox = transform.GetChild(0).GetChild(0).gameObject;
    }

    public void enterDown(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            enterIn = true;
        }
        if (context.canceled)
        {
            enterIn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Old alpha bar that we don't use
        // hpBar.barSlider.value = hp;

        // Logic for which orbs appear
        // for (int i = 0; i < healthOrbs.Length; i++)
        // {
        //     if (hp > i)
        //     {
        //         if (healthOrbs[i].gameObject.GetComponent<SpriteRenderer>().enabled == false)
        //         {
        //             // print("new orb");
        //             healthOrbs[i].gameObject.GetComponent<Animator>().SetTrigger("grow");
        //         }

        //         healthOrbs[i].gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //     }
        //     else if (hp <= i)
        //     {
        //         if (healthOrbs[i].gameObject.GetComponent<SpriteRenderer>().enabled == true)
        //         {
        //             var prefab = Instantiate(hpEffect, healthOrbs[i].gameObject.transform.position, healthOrbs[i].gameObject.transform.rotation);
        //             prefab.transform.parent = healthOrbs[i].gameObject.transform;
        //             Destroy(prefab, 1f);
        //         }
        //         healthOrbs[i].gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //     }
        // }

        // Prototype text that we don't use
        // text.text = "HP: " + hp + "/" + maxHp;

        // Respawn
        if (isDead && enterIn)
        {
            // Destroy(FindObjectOfType<dontDestroy>().gameObject);
            // SceneManager.LoadScene(FindObjectOfType<checkSaver>().lastCheckScene);
            // transform.position = FindObjectOfType<checkSaver>().lastCheckPoint;
            GetComponent<energyController>().energy = GetComponent<energyController>().maxEnergy;
            damageTaken = maxHp;
            deathAnim.SetBool("isDead", false);
            FindObjectOfType<character>().enabled = true;
            isDead = false;
        }

    }


    public void recieveDamage()
    {
        damageTaken++;

        var prefab = Instantiate(hitEffect, new Vector3(transform.position.x, transform.position.y, -5), transform.rotation);
        Destroy(prefab, 2);

        _character.playerAnim.SetTrigger("hurt");


        if (_character.IsGuardian)
        {
            if (!shieldOn)
            {
                // isMortal = false;
                // Invoke("becomeMortal", 1f);
                FindObjectOfType<modeSelector>().uiAnim.SetBool("isOn", false);
                FindObjectOfType<modeSelector>().wheelUp = false;

                // Time.timeScale = 0.2f;
                // Invoke("fixTime", 0.2f / 5f);



                if (dashScript.dashTime < dashScript.startDashTime)
                {
                    dashScript.enemyCollided();
                }
            }
        }
    }

    void fixTime()
    {
        Time.timeScale = 1;
    }


    public void recieveHealth()
    {
        damageTaken--;
        if (damageTaken < maxHp)
        {
            damageTaken = maxHp;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyWeapon" && collision.gameObject != _damageBox)
        {
            recieveDamage();
        }
        else if (collision.gameObject.layer == 18)
        {
            damageTaken = 1;
            instantDeath = true;
            recieveDamage();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.layer == 11)
        {
            if (isMortal)
            {
                recieveDamage();
                if (this.transform.position.x < other.transform.position.x)
                {
                    rb.velocity = new Vector2(-6, 6);
                }
                else if (this.transform.position.x > other.transform.position.x)
                {
                    rb.velocity = new Vector2(6, 6);
                }
            }
        }
    }

    void becomeMortal()
    {
        isMortal = true;
    }
}
