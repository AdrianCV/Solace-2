using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Movement : MonoBehaviour
{
    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y);
            }
        }
    }
}