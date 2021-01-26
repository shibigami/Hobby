using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]

public class DoorHandler : MonoBehaviour
{
    private BoxCollider2D col;

    public GameObject enterSign;

    //mage show door
    public GameObject doorIndicator;

    //priest show door
    public GameObject pointLight;

    private float showTimer;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
        enterSign.SetActive(false);

        doorIndicator.SetActive(false);
        pointLight.SetActive(false);

        showTimer = 0;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (enterSign.activeSelf)
            if (Input.GetAxis("Vertical") > 0.2f)
            {
                NextLevel();
            }

        if (showTimer>0) 
        {
            showTimer -= Time.deltaTime;
        }
        else 
        {
            if (GameData.priestJoined) pointLight.SetActive(false);
            else if (GameData.mageJoined) doorIndicator.SetActive(false);
        }

        if (doorIndicator.activeSelf) 
        {
            doorIndicator.GetComponent<RectTransform>().SetPositionAndRotation(
                player.transform.position+(transform.position- player.transform.position).normalized*1.5f,
                doorIndicator.GetComponent<RectTransform>().rotation);
        }
    }

    public void NextLevel()
    {
        GameObject.FindGameObjectWithTag("UI").GetComponent<Outputs>().ShowNextLevelWindow();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player") enterSign.SetActive(true);
        else if (collision.tag == "Mount")
        {
            if (enterSign.activeSelf) enterSign.SetActive(false);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enterSign.SetActive(false);
            GameObject.FindGameObjectWithTag("UI").GetComponent<Outputs>().nextLevelWindow.SetActive(false);
        }
    }

    public void ShowDoor(float time) 
    {
        showTimer = time;
        if (Magic.magicTypeChosen==Magic.MagicType.Priest) pointLight.SetActive(true);
        else if (Magic.magicTypeChosen == Magic.MagicType.Mage) doorIndicator.SetActive(true);
    }
}
