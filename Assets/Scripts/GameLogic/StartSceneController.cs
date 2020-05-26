using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    public Button startBtn;
    // Use this for initialization
    void Start()
    {
        startBtn.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
