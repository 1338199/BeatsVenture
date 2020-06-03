using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public float volume = 0.5f;
    public Text volumeText;
    public void ClickStart()
    {
        SceneManager.LoadScene("Level1");
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene("Setting");
        Debug.Log("Settings");
    }

    public void quitGame()
    {
        Debug.Log("Quit");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void AdjustVolume(float newVolume)
    {
        volume = newVolume;
        int showVolume = (int) (newVolume*100);
        volumeText.text = showVolume.ToString();
        Debug.Log("newVolume:" + volumeText.text);
    }
    
    public void BackToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}