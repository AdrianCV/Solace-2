using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class UIMovement : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IUpdateSelectedHandler
{
    PhotonView view;

    [SerializeField] private bool isPressed;

    [SerializeField] private bool isLeft;

    private character _player;
    // Start is called before the first frame update
    void Start()
    {
        view = GameObject.FindGameObjectWithTag("Player").GetComponent<PhotonView>();
        if (view.IsMine)
        {
            _player = view.GetComponent<character>();
        }
    }

    public void MoveLeft()
    {
        if (view.IsMine)
        {
            view.GetComponent<character>().MoveInput = -1;
        }
    }

    public void MoveRight()
    {
        if (view.IsMine)
        {
            view.GetComponent<character>().MoveInput = 1;
        }
    }

    public void OnUpdateSelected(BaseEventData data)
    {
        if (isPressed)
        {
            if (isLeft)
            {
                MoveLeft();
            }
            else
            {
                MoveRight();
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

    }

    public void Jump()
    {
        if (view.IsMine)
        {
            _player.jump();
            print(_player.gameObject.name);
        }
    }
}

