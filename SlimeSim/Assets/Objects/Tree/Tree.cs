using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject infoButton;
    public int pagesIndex;
    private bool obtained;

    // Start is called before the first frame update
    void Start()
    {
        infoButton.SetActive(false);
        obtained = false;
    }

    public void getInfo() 
    {
        GameData.AddPageToCollection(pagesIndex);
        obtained = true;
        infoButton.SetActive(false);
        if (!GameObject.FindGameObjectWithTag("UI").GetComponent<Outputs>().journalUnlocked)
            GameObject.FindGameObjectWithTag("UI").GetComponent<Outputs>().UnlockJournal();
        //clear tag for radar to stop tracking
        tag = "Untagged";
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!obtained)
            {
                if (!infoButton.activeSelf) infoButton.SetActive(true);

                if (Input.GetAxis("Vertical") > 0.1f)
                {
                    getInfo();
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(!obtained&&infoButton.activeSelf) infoButton.SetActive(false);
    }
}
