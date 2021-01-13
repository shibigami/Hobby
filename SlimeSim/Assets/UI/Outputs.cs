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
    public GameObject journalIndicator;
    private Image journalIndicatorImage;
    public GameObject[] journalPageButtons;
    public GameObject dropGoldButton;
    public GameObject goldCoin;
    public bool journalUnlocked { get; private set; }
    public bool dropGoldUnlocked { get; private set; }
    public GameObject sideKickIcon;
    public GameObject mageButton;
    public GameObject priestButton;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        GameData.init();

        //deactivate journal page buttons
        for (int i = 0; i < journalPageButtons.Length; i++) journalPageButtons[i].SetActive(false);

        //deactivate journal button
        journalUnlocked = false;

        //check if pages unlocked
        foreach (int i in GameData.collectedPages) if (i == 1) { journalUnlocked = true; break; }

        //set journal indicator image to change color
        journalIndicatorImage = journalIndicator.GetComponent<Image>();

        if (GameData.goldDropUnlocked) dropGoldUnlocked = true;
        else dropGoldUnlocked = false;

        journalButton.SetActive(false);
        dropGoldButton.SetActive(false);

        if (GameData.sideKickJoined) sideKickIcon.SetActive(true);
        else sideKickIcon.SetActive(false);

        if (GameData.mageJoined) mageButton.SetActive(true);
        else mageButton.SetActive(false);

        if (GameData.priestJoined) priestButton.SetActive(true);
        else priestButton.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");

        InvokeRepeating("UpdateUI", 0, 0.5f);
    }

    private void UpdateUI()
    {
        goldText.text = GameData.gold.ToString();
        livesText.text = GameData.lives.ToString();

        if (journalUnlocked && !journalButton.activeSelf) journalButton.SetActive(true);
        if (dropGoldUnlocked && !dropGoldButton.activeSelf) dropGoldButton.SetActive(true);
        if (GameData.sideKickJoined && !sideKickIcon.activeSelf) sideKickIcon.SetActive(true);
        if (GameData.mageJoined && !mageButton.activeSelf) mageButton.SetActive(true);
        if (GameData.priestJoined && !priestButton.activeSelf) priestButton.SetActive(true);

        if (GameObject.FindGameObjectWithTag("JournalPage"))
        {
            if (!journalIndicator.activeSelf) journalIndicator.SetActive(true);
        }
        else if(journalIndicator.activeSelf) journalIndicator.SetActive(false);
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
        GameData.UnlockGoldDrop();
        dropGoldUnlocked = true;
    }

    public void DropGold(int amount)
    {
        if (GameData.gold >= amount)
        {
            GameData.AddGold(-amount*10);
            for (int i = 0; i < amount; i++)
            {
                GameObject temp = Instantiate(goldCoin, player.transform.position, new Quaternion(0, 0, 0, 0));
                temp.GetComponent<Coin>().SetThrowned(7.5f);
            }
        }
    }
}