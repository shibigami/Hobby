using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]

public class DoorHandler : MonoBehaviour
{
    private BoxCollider2D col;

    public GameObject enterSign;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
        enterSign.SetActive(false);
    }

    private void Update()
    {
        if (enterSign.activeSelf)
            if (Input.GetAxis("Vertical") > 0.2f)
            {
                GameData.NextLevel();
                try
                {
                    SceneManager.LoadScene(GameData.currentLevel.ToString());
                }
                catch
                {
                    Debug.Log("Invalid Scene");
                }
            }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player") enterSign.SetActive(true); 
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") enterSign.SetActive(false);
    }
}
