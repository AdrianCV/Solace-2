using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightCheck : MonoBehaviour
{
    [SerializeField] private Sprite _darkBackground;
    [SerializeField] private Sprite _lightBackground;

    [SerializeField] private SpriteRenderer _background;
    // Start is called before the first frame update
    void Start()
    {
        InputSystem.EnableDevice(LightSensor.current);

        if (LightSensor.current?.lightLevel.ReadValue() < 200)
        {
            _background.sprite = _darkBackground;
        }
        else
        {
            _background.sprite = _lightBackground;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
