using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIMovement : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IUpdateSelectedHandler, IPunObservable
{
    PhotonView view;

    [SerializeField] private bool isPressed;

    [SerializeField] private bool isLeft;
    [SerializeField] private bool isUp;
    [SerializeField] private bool isDown;

    [SerializeField] private GameObject _leaveMenu;

    private character _player;
    MainManager _manager;
    AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        view = GameObject.FindGameObjectWithTag("Player").GetComponent<PhotonView>();
        if (view.IsMine)
        {
            _player = view.GetComponent<character>();
            _manager = GameObject.FindObjectOfType<MainManager>();
            _audioSource = _manager.GetComponent<AudioSource>();
        }
    }

    public void MoveLeft()
    {
        view.GetComponent<character>().MoveInput = -1;
        view.GetComponent<character>().SavedInput = -1;
    }

    public void MoveRight()
    {

        view.GetComponent<character>().MoveInput = 1;
        view.GetComponent<character>().SavedInput = 1;
    }

    public void LookUp()
    {
        view.GetComponent<character>().LookInput = 1;
    }

    public void LookDown()
    {
        view.GetComponent<character>().LookInput = -1;
    }

    public void OnUpdateSelected(BaseEventData data)
    {
        if (view.IsMine)
        {
            if (isPressed)
            {
                if (isLeft)
                {
                    MoveLeft();
                    // view.RPC("MoveLeft", RpcTarget.Others);
                }
                else if (isUp)
                {
                    LookUp();
                }
                else if (isDown)
                {
                    LookDown();
                }
                else
                {
                    MoveRight();
                }
            }
        }
    }
    public void OnPointerDown(PointerEventData data)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        isPressed = false;
        view.GetComponent<character>().MoveInput = 0;
        view.GetComponent<character>().LookInput = 0;
    }

    public void Jump()
    {
        if (view.IsMine)
        {
            _player.jump();
            // print(_player.gameObject.name);
        }
    }

    public void Attack()
    {
        if (_player.IsGuardian)
        {
            _player.GetComponent<parryAttack>().hit();
        }
        else
        {
            _player.IceClownAttack();
        }
    }

    public void LeaveMenu()
    {
        _audioSource.Play();
        _leaveMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        _audioSource.Play();
        _leaveMenu.SetActive(false);
    }

    public void LeaveGame()
    {
        _audioSource.Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // if (stream.IsWriting)
        // {
        //     stream.SendNext(view.GetComponent<character>().SavedInput);
        // }
        // else
        // {
        //     view.GetComponent<character>().SavedInput = (int)stream.ReceiveNext();
        // }
    }
}

