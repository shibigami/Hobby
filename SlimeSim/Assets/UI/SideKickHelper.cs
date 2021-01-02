using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideKickHelper : MonoBehaviour
{
    public GameObject balloon, text, normalChestIcon, bigChestIcon;

    // Start is called before the first frame update
    void Start()
    {
        balloon.SetActive(false);
        text.SetActive(false);
        normalChestIcon.SetActive(false);
        bigChestIcon.SetActive(false);

        InvokeRepeating("UpdateLookForChest", 5f, 1.5f);
    }

    private void UpdateLookForChest()
    {
        if (!IsThereUnopenedChests())
        {
            balloon.SetActive(false);
            text.SetActive(false);
            normalChestIcon.SetActive(false);
        }
        else
        {
            balloon.SetActive(true);
            text.SetActive(true);
            normalChestIcon.SetActive(true);
        }
    }

    private bool IsThereUnopenedChests()
    {
        if (GameObject.FindGameObjectWithTag("Chest"))
        {
            GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");
            foreach (GameObject chest in chests)
                if (!chest.GetComponent<Chest>().opened) return true;

            return false;
        }
        else return false;
    }
}