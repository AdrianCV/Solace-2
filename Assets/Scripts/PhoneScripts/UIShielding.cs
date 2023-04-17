using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIShielding : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IUpdateSelectedHandler, IPunObservable
{
    private PhotonView view;
    private character _player;

    [SerializeField] private bool isPressed;


    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        _player.Shielding = true;
        StartCoroutine(ShieldTimer());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopAllCoroutines();
        isPressed = false;
        _player.Shielding = false;
    }

    public void OnUpdateSelected(BaseEventData eventData)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        view = GameObject.FindGameObjectWithTag("Player").GetComponent<PhotonView>();
        if (view.IsMine)
        {
            _player = view.GetComponent<character>();
        }
    }

    IEnumerator ShieldTimer()
    {
        yield return new WaitForSeconds(5);
        isPressed = false;
        _player.Shielding = false;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
