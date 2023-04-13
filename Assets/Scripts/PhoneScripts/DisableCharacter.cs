using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableCharacter : MonoBehaviour
{
    [SerializeField] private MainManager _stats;
    [SerializeField] private GameObject _boughtSprite;
    [SerializeField] private Button _buyButton;

    private void Awake()
    {
        _stats = GameObject.FindObjectOfType<MainManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_stats.BoughtCharacters.Count == 0)
        {
            _boughtSprite.SetActive(true);
            _buyButton.interactable = false;
        }
        else
        {
            _boughtSprite.SetActive(false);
            _buyButton.interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
