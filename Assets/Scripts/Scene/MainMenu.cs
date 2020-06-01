using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void ClickStart()
    {
        SceneManager.LoadScene("Level1");
    }

    public void GoToSettings()
    {
        Debug.Log("Settings");
    }

    public void quitGame()
    {
        Debug.Log("Quit");;
    }
    
}