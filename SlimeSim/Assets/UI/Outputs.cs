using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Outputs : MonoBehaviour
{
    public Text goldText;
    public Text livesText;
    public Text pageText;
    public GameObject journalButton;
    public GameObject[] journalPageButtons;
    public GameObject dropGoldButton;
    public GameObject goldCoin;
    public bool journalUnlocked { get; private set; }
    public bool dropGoldUnlocked { get; private set; }
    public GameObject sideKickIcon;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < journalPageButtons.Length; i++) journalPageButtons[i].SetActive(false);

        journalUnlocked = false;
        dropGoldUnlocked = false;

        journalButton.SetActive(false);
        dropGoldButton.SetActive(false);

        sideKickIcon.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");

        InvokeRepeating("UpdateUI", 0, 0.5f);
    }

    void UpdateUI()
    {
        goldText.text = GameData.gold.ToString();
        livesText.text = GameData.lives.ToString();

        if (journalUnlocked && !journalButton.activeSelf) journalButton.SetActive(true);
        if (dropGoldUnlocked && !dropGoldButton.activeSelf) dropGoldButton.SetActive(true);
        if (GameData.sideKickJoined && !sideKickIcon.activeSelf) sideKickIcon.SetActive(true);
    }

    private void Update()
    {
    }

    public void SetPageToShow(int index) 
    {
        pageText.text = Journal.pages[index];
    }

    public void UpdateAvailablePages() 
    {
        for (int i = 0; i < Journal.numberOfPages; i++) 
        {
            if (GameData.collectedPages[i] == 1) journalPageButtons[i].SetActive(true);
            else journalPageButtons[i].SetActive(false);
        }
    }

    public void UnlockJournal() 
    {
        journalUnlocked = true;
    }

    public void UnlockDropGold() 
    {
        dropGoldUnlocked = true;
    }

    public void DropGold(int amount) 
    {
        if (GameData.gold >= amount) 
        {
            GameData.AddGold(-amount);
            for (int i = 0; i < amount; i++)
            {
                GameObject temp=Instantiate(goldCoin,player.transform.position,new Quaternion(0,0,0,0));
                temp.GetComponent<Coin>().SetThrowned(20f);
            }
        }
    }
}
