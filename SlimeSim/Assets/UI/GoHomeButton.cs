using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHomeButton : MonoBehaviour
{
    public GameObject confirmWindow;

    private void Start()
    {
        confirmWindow.SetActive(false);
    }

    public void GoHomeNoSave() 
    {
        GameData.ReLoadData();
        if(SceneManager.GetActiveScene().name!="Hub") SceneManager.LoadScene("Hub");
    }
}
