using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class parryAttack : MonoBehaviour
{
    public GameObject shield;
    private float shieldLocation;

    [SerializeField] private float cdTime = 0.5f;
    [SerializeField] private float upTime = 0.5f;
    private float coolDown;
    private bool cooldownActive = false;

    public Vector2 attackArea = new Vector2(0.6f, -0.15f);
    public float attackSize;
    public LayerMask enemyLayer;
    public LayerMask floorLayer;
    public LayerMask itemLayer;

    public GameObject hitAnim;
    [SerializeField] private GameObject soul;

    private character _charScript;

    // Start is called before the first frame update
    void Start()
    {
        coolDown = cdTime + upTime;
        shieldLocation = shield.transform.localPosition.x;
        floorLayer = ~floorLayer;
        _charScript = GetComponent<character>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(this.transform.position.x, this.transform.position.y) + attackArea, attackSize);
    }

    public void hit()
    {
        if (_charScript.MoveInput == 0 && _charScript.LookInput == 0)
        {
            Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(new Vector2(this.transform.position.x, this.transform.position.y) + attackArea, attackSize, enemyLayer);

            if (GetComponent<parryAttack>().enabled == true && cooldownActive == false)
            {
                hitAnim.GetComponent<Animator>().SetTrigger("hit");
                hitAnim.GetComponent<AudioSource>().Play();


                // bool enemyFound = false;

                // foreach (Collider2D fiend in enemyColliders)
                // {
                //     enemyFound = true;
                //     // Hits enemies
                //     if (fiend.GetComponent<damagePlayer>() != null)
                //     {
                //         fiend.GetComponent<damagePlayer>().recieveDamage();
                //         // print(fiend.gameObject.name);
                //         var prefab = Instantiate(soul, fiend.transform.position, fiend.transform.rotation);
                //         // FindObjectOfType<energyController>().recieveEnergy();
                //         // if (this.transform.position.x < fiend.transform.position.x)
                //         // {
                //         //     fiend.GetComponent<damagePlayer>().hDir = 1;
                //         // }
                //         // else if (this.transform.position.x > fiend.transform.position.x)
                //         // {
                //         //     fiend.GetComponent<damagePlayer>().hDir = -1;
                //         // }
                //     }
                // }

                // if (enemyFound)
                // {
                //     FindObjectOfType<CameraScript>().dealDamage();
                // }

                // flips the animations thingy
                EndOfAttack();
                CheckRotation();
            }
        }
        else if (_charScript.MoveInput != 0)
        {
            EndOfAttack();
            CheckRotation();
        }
        else if (_charScript.LookInput != 0)
        {
            EndOfAttack();
            CheckRotation();
        }
    }

    void EndOfAttack()
    {
        cooldownActive = true;
        Invoke("closeShield", upTime);
        Invoke("endCool", coolDown);

    }

    void CheckRotation()
    {
        if (!this.GetComponent<character>().stickRender.flipX)
        {
            hitAnim.GetComponent<SpriteRenderer>().flipX = false;
            hitAnim.transform.localPosition = new Vector3(0.87f, 0.18f, 0);
        }
        else
        {
            hitAnim.GetComponent<SpriteRenderer>().flipX = true;
            hitAnim.transform.localPosition = new Vector3(-0.87f, 0.18f, 0);
        }
    }

    void Update()
    {
        /*
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(new Vector2(this.transform.position.x, this.transform.position.y) + attackArea, attackSize, enemyLayer);
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && cooldown == false)
        {
            hitAnim.SetActive(true);
            cooldown = true;
            shield.SetActive(true);
            Invoke("closeShield", upTime);
            Invoke("endCool", coolDown);
            Invoke("disableAnim", 0.32f);

            foreach (Collider2D fiend in enemyColliders)
            {
                // Hits enemies
                if(fiend.GetComponent<EnemyDamageTaken>() != null && fiend.isTrigger)
                {
                    fiend.GetComponent<EnemyDamageTaken>().recieveDamage(1);
                    // print(fiend.gameObject.name);
                    var prefab = Instantiate(soul, fiend.transform.position, fiend.transform.rotation);
                    // FindObjectOfType<energyController>().recieveEnergy();
                    if (this.transform.position.x < fiend.transform.position.x)
                    {
                        fiend.GetComponent<EnemyDamageTaken>().hDir = 1;
                    }
                    else if (this.transform.position.x > fiend.transform.position.x)
                    {
                        fiend.GetComponent<EnemyDamageTaken>().hDir = -1;
                    }
                }
            }

            // Hits object that you can destroy
            Collider2D[] itemColliders = Physics2D.OverlapCircleAll(new Vector2(this.transform.position.x, this.transform.position.y) + attackArea, attackSize, itemLayer);
            foreach (Collider2D items in itemColliders)
            {
                if (items.GetComponent<Destroyable>() != null)
                {
                    items.GetComponent<Destroyable>().destroyObject();
                }
            }

            // flips the animations thingy

            if(this.GetComponent<character>().looking == 1)
            {
                hitAnim.GetComponent<SpriteRenderer>().flipX = false;
                hitAnim.transform.localPosition = new Vector3(0.87f, 0.18f, 0);
            } else
            {
                hitAnim.GetComponent<SpriteRenderer>().flipX = true;
                hitAnim.transform.localPosition = new Vector3(-0.87f, 0.18f, 0);
            }

        }

        */

        if (this.transform.GetComponent<character>().stickRender.flipX)
        {
            shield.transform.localPosition = new Vector2(-Mathf.Abs(shieldLocation), 0);
            attackArea = new Vector2(-Mathf.Abs(attackArea.x), -0.15f);
        }
        else
        {
            shield.transform.localPosition = new Vector2(Mathf.Abs(shieldLocation), 0);
            attackArea = new Vector2(Mathf.Abs(attackArea.x), -0.15f);
        }

    }
    public void closeShield()
    {
        shield.SetActive(false);
    }

    public void endCool()
    {
        cooldownActive = false;
    }
}
