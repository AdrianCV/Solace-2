using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    AudioSource _source;
    [SerializeField] AudioClip _attackAudio;
    [SerializeField] AudioClip _damageAudio;


    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AttackSound()
    {
        _source.PlayOneShot(_attackAudio);
    }

    void TakeDamageSound()
    {
        _source.PlayOneShot(_damageAudio);
    }
}
