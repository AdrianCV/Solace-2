using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Cinemachine;
using System;

public class character : MonoBehaviourPunCallbacks, IPunObservable
{
    public AudioSource AudioSource;

    public AudioClip _attack1;
    public AudioClip _attack2;
    public AudioClip _hit;
    public AudioClip _special;

    private float coolDown;
    public bool cooldownActive = false;

    [SerializeField] private float cdTime = 0.5f;
    [SerializeField] private float upTime = 0.5f;

    public float speed = 0.1f;
    [SerializeField] private float m_JumpForce = 400f;

    const float k_GroundedRadius = .01f;
    private Rigidbody2D rb;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public float groundedHeight = 0.51f;
    public float checkRate = 1.0f;
    public bool grounded = false;
    public bool onWall = false;
    public LayerMask groundLayer;
    public float heightOffset = 0.25f;
    public bool lookingLeft = false;

    private float startHeight = 10000;
    public float jumpHeight = 4;

    public SpriteRenderer stickRender;
    public Animator playerAnim;
    public bool isSitting = false;
    [SerializeField] private float wallDistance = 0.325f;

    private IEnumerator coroutine;

    public float MoveInput;

    public Vector2 MovementVector;

    [HideInInspector] public float SavedInput = 1;
    [HideInInspector] public float LookInput;

    public bool IsGuardian;

    [HideInInspector] public PhotonView view;
    public CinemachineTargetGroup Group;

    public bool Shielding;

    private const byte WON_EVENT = 0;


    private MobileJoystick joystick;

    public event Action<Vector2> OnMovement;


    private void Start()
    {
        joystick = GameObject.FindObjectOfType<MobileJoystick>();
        joystick.OnMove += Move;

        AudioSource = GetComponent<AudioSource>();

        Group = GameObject.FindObjectOfType<CinemachineTargetGroup>();
        Group.AddMember(transform, 1, 0);
        // _cam.GetComponent <
        // - After 0 seconds, prints "Starting 0.0"
        // - After 0 seconds, prints "Before WaitAndPrint Finishes 0.0"
        // - After 2 seconds, prints "WaitAndPrint 2.0"
        //print("Starting " + Time.time);

        // Start function WaitAndPrint as a coroutine.

        coolDown = cdTime + upTime;


        view = GetComponent<PhotonView>();
        AddObservable();

        coroutine = WaitAndPrint(2.0f);
        StartCoroutine(coroutine);

        //print("Before WaitAndPrint Finishes " + Time.time);
    }

    private void Move(Vector2 input)
    {
        MovementVector = input;
        OnMovement?.Invoke(MovementVector);
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            //print("WaitAndPrint " + Time.time);
        }
    }

    void Update()
    {
        transform.GetChild(0).localScale = new Vector3(SavedInput, 1, 1);

        if (view.IsMine)
        {// MoveInput = Input.GetAxis("Horizontal");
            MoveInput = MovementVector.x;
            LookInput = MovementVector.y;
            playerAnim.SetBool("grounded", grounded);

            if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("sit"))
            {
                isSitting = true;
            }
            else
            {
                isSitting = false;
            }

            playerAnim.SetFloat("velocity", rb.velocity.y);
            GroundCheck();


            // Jumping
            /*
            if (!shockShieldOn && grounded && !isSitting)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    startHeight = this.transform.position.y;
                    rb.velocity = new Vector2(rb.velocity.x, m_JumpForce);
                }
            }
            */
            // Moving left
            // if (!onWall && FindObjectOfType<dashMove>().isDashing == false && !isSitting && FindObjectOfType<PauseMenu>().paused == false && FindObjectOfType<PauseMenu>().inventoryUp == false)
            // {
            if (MoveInput < 0)
            {
                // stickRender.flipX = true;
                print("Looking left");
                lookingLeft = true;
                playerAnim.SetBool("running", true);
                SavedInput = -1;
            }
            // Moving right
            else if (MoveInput > 0)
            {
                // stickRender.flipX = false;
                lookingLeft = false;
                playerAnim.SetBool("running", true);
                SavedInput = 1;
            }
            // }
            else
            {
                playerAnim.SetBool("running", false);
            }

            if (this.transform.position.y > startHeight + jumpHeight)
            {
                //rb.velocity = new Vector2(rb.velocity.x, 0);
                startHeight = 10000;
            }
        }
        else
        {
            // _cam.gameObject.SetActive(false);
            gameObject.layer = 11;
            gameObject.tag = "Enemy";
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public void jump()
    {
        // print("jump");
        // Jumping

        if (grounded)
        {
            startHeight = this.transform.position.y;
            rb.velocity = new Vector2(rb.velocity.x, m_JumpForce);

            playerAnim.SetTrigger("jump");
        }

    }

    IEnumerator waitWall()
    {
        yield return new WaitForSeconds(0.02f);

        //After we have waited 5 seconds print the time again.
        //rb.velocity = new Vector2(rb.velocity.x, 10);
    }

    void wallCheck()
    {
        if (Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), -Vector3.right, wallDistance, groundLayer) && !grounded)
        {
            onWall = true;
            stickRender.flipX = false;
            lookingLeft = true;
        }
        else if (Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.right, wallDistance, groundLayer) && !grounded)
        {
            onWall = true;
            stickRender.flipX = true;
            lookingLeft = false;
        }
        else
        {
            onWall = false;
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), -Vector3.right * wallDistance, Color.yellow);
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.right * wallDistance, Color.yellow);
        }
    }

    // public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance = Mathf.Infinity, groundLayer, float minDepth = -Mathf.Infinity, float maxDepth = Mathf.Infinity);

    void GroundCheck()
    {
        grounded = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + heightOffset), groundedHeight, groundLayer);

        /*if (Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + heightOffset, transform.position.z), Vector3.down, groundedHeight + heightOffset, groundLayer))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }*/
    }

    public void IceClownAttack()
    {
        if (!cooldownActive)
        {
            Shielding = false;
            if (LookInput == 0 && MoveInput == 0 || !grounded && MoveInput != 0)
            {
                playerAnim.SetTrigger("attacking");
                AudioSource.PlayOneShot(_attack1);
            }
            else if (MoveInput != 0 && grounded)
            {
                StartCoroutine(DashAttack());
                playerAnim.SetTrigger("runHit");
                AudioSource.PlayOneShot(_attack2);
            }
            else if (LookInput > 0 && !grounded)
            {
                playerAnim.SetTrigger("upHit");
                AudioSource.PlayOneShot(_attack1);
            }
            else if (LookInput < 0 && !grounded)
            {
                playerAnim.SetTrigger("downHit");
                AudioSource.PlayOneShot(_attack2);
            }
        }
    }

    public void EndOfAttack()
    {
        cooldownActive = true;
        // Invoke("closeShield", upTime);
        Invoke("endCool", coolDown);
    }

    IEnumerator DashAttack()
    {
        speed *= 1.2f;
        yield return new WaitForSeconds(0.3f);
        speed /= 1.2f;
    }

    public void endCool()
    {
        cooldownActive = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + heightOffset, transform.position.z), groundedHeight);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        if (!isSitting && MoveInput != 0)
        {
            transform.position += (Vector3.right * MoveInput * speed);
            // transform.position += Vector3.right * MoveInput * speed;
            playerAnim.SetFloat("speed", MoveInput);
        }
        else
        {
            playerAnim.SetFloat("speed", 0);
        }
    }

    private void AddObservable()
    {
        if (!view.ObservedComponents.Contains(this))
        {
            view.ObservedComponents.Add(this);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext((float)SavedInput);
            stream.SendNext((float)MoveInput);
            stream.SendNext((bool)Shielding);

            // Lag compensation
            // stream.SendNext(rb.position);
            // stream.SendNext(rb.rotation);
            // stream.SendNext(rb.velocity);
        }
        else
        {
            SavedInput = (float)stream.ReceiveNext();
            MoveInput = (float)stream.ReceiveNext();
            Shielding = (bool)stream.ReceiveNext();

            // rb.position = (Vector3)stream.ReceiveNext();
            // rb.rotation = (float)stream.ReceiveNext();
            // rb.velocity = (Vector3)stream.ReceiveNext();

            // float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            // rb.position += rb.velocity * lag;
        }
    }
}