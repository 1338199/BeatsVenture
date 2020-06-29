using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts.Player;

public class MainMenu : MonoBehaviour
{
    public float volume = 0.5f;
    public Text volumeText;
    public bool inGameScene = false;
    public bool isMainMenu = false;
    public Slider volumeSlider;

    void Start()
    {
        if (inGameScene)
        {
            gameObject.SetActive(false);
        }

        if (!isMainMenu)
        {        
            volume = PlayerPrefs.GetFloat("volume");
            volumeSlider.value = volume;
        }

    }

    public void ShowMenu()
    {
        gameObject.SetActive(true);
        GameController.pauseGame();
    }
    
    public void CloseMenu()
    {
        Debug.Log(volume);
        AdjustVolume(volume);
        gameObject.SetActive(false);
        GameController.resumeGame();
    }
    
    public void ClickStart()
    {
        skillUtils.saveSkills(new bool[4] { false, false, false, false });
        MoneyUtils.saveMoney(100);
        SkillNumUtils.saveSkillNum(0);
        PlayerPrefs.SetFloat("playerHP", 100);
        PlayerPrefs.SetInt("damage", 30);
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
        PlayerPrefs.SetFloat("volume", volume);
    }
    
    public void BackToMenu()
    {
        AdjustVolume(volume);
        SceneManager.LoadScene("StartMenu");
    }
}