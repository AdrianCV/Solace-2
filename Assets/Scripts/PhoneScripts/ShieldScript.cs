using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    Animator _animator;
    [SerializeField] character _character;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("isOn", _character.Shielding);
    }

    private void OnEnable()
    {

    }
}
