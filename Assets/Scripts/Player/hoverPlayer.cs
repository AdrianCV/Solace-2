using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverPlayer : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;
    [SerializeField] private Animator hoverAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = transform.parent.GetComponent<character>().playerAnim;
        hoverAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAnim.GetBool("grounded") == true && playerAnim.GetBool("sitting") == false)
        {
            hoverAnim.SetBool("hover", true);
        }
        else
        {
            hoverAnim.SetBool("hover", false);
        }
    }
}
