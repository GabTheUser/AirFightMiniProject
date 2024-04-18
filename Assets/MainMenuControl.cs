using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    public TextMeshProUGUI endText;
    public GameObject mainMenu, endScreen, controlUI;
    public GameObject[] allPlanes;
    public AudioSource[] soundObjects;
    private void Start()
    {
        Time.timeScale = 0;
    }
    public void StartGame()
    {
        Time.timeScale = 1;
        for (int i = 0; i < soundObjects.Length; i++)
        {
            soundObjects[i].enabled = true;
        }
        mainMenu.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void EndScreenStats(bool win)
    {
        Time.timeScale = 0;
        for (int i = 0; i < soundObjects.Length; i++)
        {
            soundObjects[i].enabled = false;
        }
        endScreen.SetActive(true);
        controlUI.SetActive(false);
        for (int i = 0; i < allPlanes.Length; i++)
        {
            allPlanes[i].SetActive(false);
        }    
        if (win)
        {
            endText.text = "You win!";
        }
        else
        {
            endText.text = "You lose!";
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
