using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject infoButton;
    public int pageNumber;
    private bool obtained;

    // Start is called before the first frame update
    void Start()
    {
        infoButton.SetActive(false);
        obtained = false;
        pageNumber = Mathf.Clamp(pageNumber, 0, Journal.numberOfPages);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void getInfo() 
    {
        GameData.AddPageToCollection(pageNumber);
        obtained = true;
        infoButton.SetActive(false);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!obtained)
        {
            if(!infoButton.activeSelf) infoButton.SetActive(true);

            if (Input.GetAxis("Vertical") > 0.1f)
            {
                getInfo();
                if (!GameObject.FindGameObjectWithTag("UI").GetComponent<Outputs>().journalUnlocked)
                    GameObject.FindGameObjectWithTag("UI").GetComponent<Outputs>().UnlockJournal();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(!obtained&&infoButton.activeSelf) infoButton.SetActive(false);
    }
}
