using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScriot : MonoBehaviour
{
    [SerializeField] private GameObject rankedSelect;
    [SerializeField] private GameObject defaultMenu;
    public void StartButton()
    {
        rankedSelect.SetActive(true);
        defaultMenu.SetActive(false);
    }

    public void RankedButtons()
    {
        SceneManager.LoadScene("Loading");
    }
}
