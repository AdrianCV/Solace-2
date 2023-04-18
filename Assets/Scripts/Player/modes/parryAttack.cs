using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class parryAttack : MonoBehaviour
{
    public GameObject shield;
    private float shieldLocation;

    public Vector2 attackArea = new Vector2(0.6f, -0.15f);
    public float attackSize;
    public LayerMask enemyLayer;
    public LayerMask floorLayer;
    public LayerMask itemLayer;

    public GameObject hitAnim;
    [SerializeField] private GameObject soul;

    [SerializeField] private character _charScript;

    // Start is called before the first frame update
    void Start()
    {
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
        if (!_charScript.cooldownActive)
        {
            _charScript.Shielding = false;
            if (_charScript.MoveInput == 0 && _charScript.LookInput == 0)
            {
                hitAnim.GetComponent<Animator>().SetTrigger("hit");
                _charScript.AudioSource.PlayOneShot(_charScript._attack1);
            }
            else if (_charScript.MoveInput != 0)
            {
                hitAnim.GetComponent<Animator>().SetTrigger("runHit");
                _charScript.AudioSource.PlayOneShot(_charScript._attack2);
            }
            else if (_charScript.LookInput > 0 && !_charScript.grounded)
            {
                hitAnim.GetComponent<Animator>().SetTrigger("upHit");
                _charScript.AudioSource.PlayOneShot(_charScript._attack1);
            }
            else if (_charScript.LookInput < 0 && !_charScript.grounded)
            {
                hitAnim.GetComponent<Animator>().SetTrigger("downHit");
                _charScript.AudioSource.PlayOneShot(_charScript._attack2);
            }

            // hitAnim.GetComponent<AudioSource>().Play();

            // flips the animations thingy
            _charScript.EndOfAttack();
            CheckRotation();
        }
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
}
