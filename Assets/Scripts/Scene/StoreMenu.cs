using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreMenu : MonoBehaviour
{
    private bool isShown = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
    }

    public void showStoreMenu()
    {
        isShown = true;
        Debug.Log("show shop2");
        gameObject.SetActive(true);
    }

    public void closeStoreMenu()
    {
        isShown = false;
        gameObject.SetActive(false);
    }
}
