using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class damagePlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    public float damageTaken = 0;
    public float lives = 1;
    private Rigidbody2D rb;
    public GameObject hitEffect;

    public Animator deathAnim;
    public bool isDead = false;
    public bool isMortal = true;
    public bool shieldOn = false;
    private bool enterIn = false;
    private bool instantDeath = false;

    [SerializeField] private bool _won;
    private bool _over;

    private dashMove dashScript;


    private character _character;

    [SerializeField] private GameObject _damageBox;

    public UIDamage uiDamage;

    public const byte DAMAGE_EVENT = 0;
    public const byte GAMEOVER_EVENT = 1;

    private GameObject _otherPlayer;

    private GameObject ui;

    [SerializeField] private float weight;

    [SerializeField] private MainManager _stats;

    [SerializeField] private GameObject _wonText;
    // [SerializeField] private GameObject _lostText;

    [SerializeField] private GameObject _controls;


    // Start is called before the first frame update
    void Start()
    {
        _stats = GameObject.FindObjectOfType<MainManager>();
        rb = this.GetComponent<Rigidbody2D>();
        dashScript = FindObjectOfType<dashMove>();

        _character = GetComponent<character>();



        if (_character.view.IsMine)
        {
            ui = GameObject.FindGameObjectWithTag("Canvas");


            uiDamage = ui.transform.GetChild(PhotonNetwork.PlayerList.Length - 1).GetComponent<UIDamage>();

            _wonText = GameObject.FindGameObjectWithTag("WonText");
            _wonText.SetActive(false);


            _controls = GameObject.FindGameObjectWithTag("Controls");
        }
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
        if (lives <= 0)
        {
            _won = false;
        }
        // Prototype text that we don't use
        // text.text = "HP: " + hp + "/" + maxHp;

        if (_character.view.IsMine)
        {
            _otherPlayer = GameObject.FindGameObjectWithTag("Enemy");

            if (_otherPlayer != null)
            {
                foreach (Transform child in ui.transform)
                {
                    if (child.GetComponent<UIDamage>() != uiDamage)
                    {
                        _otherPlayer.GetComponent<damagePlayer>().uiDamage = child.GetComponent<UIDamage>();
                    }
                }
            }
        }

        // Respawn
        if (isDead && enterIn)
        {
            // Destroy(FindObjectOfType<dontDestroy>().gameObject);
            // SceneManager.LoadScene(FindObjectOfType<checkSaver>().lastCheckScene);
            // transform.position = FindObjectOfType<checkSaver>().lastCheckPoint;
            GetComponent<energyController>().energy = GetComponent<energyController>().maxEnergy;
            deathAnim.SetBool("isDead", false);
            FindObjectOfType<character>().enabled = true;
            isDead = false;
        }
    }


    public void recieveDamage(GameObject attacker)
    {
        if (!_character.Shielding)
        {
            damageTaken += 10;

            float knockback = (((damageTaken / 10) + 2) * 100) / weight * (1 - weight / 100) * 1.2f;

            Vector2 dir = ((transform.position - attacker.transform.position).normalized + Vector3.up * 1.5f).normalized;

            rb.AddForce(dir * knockback);

            PhotonNetwork.RaiseEvent(DAMAGE_EVENT, damageTaken, RaiseEventOptions.Default, SendOptions.SendReliable);

            _character.playerAnim.SetTrigger("hurt");
            _character.AudioSource.PlayOneShot(_character._hit);
        }


        var prefab = Instantiate(hitEffect, new Vector3(transform.position.x, transform.position.y, -5), transform.rotation);
        Destroy(prefab, 2);
    }

    void fixTime()
    {
        Time.timeScale = 1;
    }


    public void recieveHealth()
    {
        damageTaken--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyWeapon" && collision.gameObject != _damageBox)
        {
            recieveDamage(collision.gameObject);
        }
        else if (collision.gameObject.layer == 18)
        {
            damageTaken = 1;
            instantDeath = true;
            recieveDamage(collision.gameObject);
        }
        else if (collision.tag == "DeathBox")
        {
            damageTaken = 0;
            lives--;
            if (lives == 0 && !_over)
            {

            }
            else
            {
                transform.position = new Vector2(0, 0);
            }

            CalculateRank();

            PhotonNetwork.RaiseEvent(GAMEOVER_EVENT, _stats.RankedPoint, RaiseEventOptions.Default, SendOptions.SendReliable);
            GameOver();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.layer == 11)
        {
            if (isMortal)
            {
                recieveDamage(other.gameObject);
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

    void CalculateRank()
    {
        _stats.RankedPoint += (int)(_stats.RankConstant * (_won ? 1 : 0 - CalculateEloExpectedScore(_stats.RankedPoint, 1900)));

        // _stats.RankedPoint += (int)(_stats.RankConstant * (_won ? 1 : 0 - CalculateEloExpectedScore(_stats.RankedPoint, _stats.OpponentRankedPoints)));
    }

    public float CalculateEloExpectedScore(float playerRating, float opponentRating)
    {
        float exponent = (opponentRating - playerRating) / 400f;
        float expectedScore = 1f / (1f + Mathf.Pow(10f, exponent));
        return expectedScore;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventRecieved;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventRecieved;
    }

    private void NetworkingClient_EventRecieved(EventData obj)
    {
        if (obj.Code == DAMAGE_EVENT)
        {
            uiDamage.transform.GetChild(0).GetComponent<TMP_Text>().text = damageTaken + "%";
        }

        if (obj.Code == GAMEOVER_EVENT)
        {
            GameOver();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(damageTaken);
        }
        else
        {
            damageTaken = (float)stream.ReceiveNext();
        }
    }

    void GameOver()
    {
        if (!_over)
        {
            _character.Group.RemoveMember(transform);
            _over = true;
            StartCoroutine(HandleGameOver());
        }
    }

    IEnumerator HandleGameOver()
    {
        yield return new WaitForSeconds(1);
        transform.position = new Vector2(0, 0);
        if (lives > 0)
        {
            _won = true;
        }

        if (_character.view.IsMine)
        {
            // print("wo");

            if (!_won)
            {
                _wonText.SetActive(true);
                _wonText.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                // _wonText.transform.GetChild(0).GetChild(4).GetChild(2).gameObject.SetActive(true);
                _controls.SetActive(false);
                transform.GetChild(0).gameObject.SetActive(false);
                _character.enabled = false;
                _stats.SoftCoins -= _stats.BetAmount;
                _stats.SoftCoins = Mathf.Max(_stats.SoftCoins, 0);
            }
            else
            {
                _wonText.SetActive(true);
                _wonText.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                // _wonText.transform.GetChild(0).GetChild(3).GetChild(2).gameObject.SetActive(true);
                _stats.SoftCoins += _stats.BetAmount;
            }
        }
        else
        {
            if (!_won)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
