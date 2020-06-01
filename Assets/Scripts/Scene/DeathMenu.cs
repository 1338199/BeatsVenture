using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    public Image bgImg;
    private bool isShown = true;
    private float transition = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShown)
            return;
        transition += Time.deltaTime;
        bgImg.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);
    }

    public void showDeathMenu()
    {
        isShown = true;
        gameObject.SetActive(true);
    }

    public void Restart()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void quitGame()
    {
        ;
    }
    
}
