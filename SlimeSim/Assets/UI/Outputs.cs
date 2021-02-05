using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Outputs : MonoBehaviour
{
    public Text goldText;
    public Text livesText;
    public Text pageText;
    public GameObject journalButton;
    public GameObject journalIndicator;
    public GameObject[] journalPageButtons;
    public GameObject dropGoldButton;
    public GameObject goldCoin;
    public bool journalUnlocked { get; private set; }
    public GameObject sideKickIcon;
    public GameObject mageButton;
    public GameObject priestButton;
    public GameObject inventoryButton;
    public GameObject nextLevelWindow;
    public GameObject goHomeButton;

    private GameObject player;
    private PlayerController playerController;

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

        journalButton.SetActive(false);
        inventoryButton.SetActive(false);
        dropGoldButton.SetActive(false);

        if (GameData.sideKickJoined) sideKickIcon.SetActive(true);
        else sideKickIcon.SetActive(false);

        if (GameData.mageJoined) mageButton.SetActive(true);
        else mageButton.SetActive(false);

        if (GameData.priestJoined) priestButton.SetActive(true);
        else priestButton.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        InvokeRepeating("UpdateUI", 0, 0.5f);
    }

    private void Update()
    {
        goldText.text = GameData.gold.ToString();
    }

    private void UpdateUI()
    {
        livesText.text = GameData.lives.ToString();

        if (journalUnlocked && !journalButton.activeSelf) journalButton.SetActive(true);
        if (GameData.goldDropUnlocked && !dropGoldButton.activeSelf) dropGoldButton.SetActive(true);
        if (GameData.inventoryUnlocked && !inventoryButton.activeSelf) inventoryButton.SetActive(true);
        if (GameData.sideKickJoined && !sideKickIcon.activeSelf) sideKickIcon.SetActive(true);
        if (GameData.mageJoined && !mageButton.activeSelf) mageButton.SetActive(true);
        if (GameData.priestJoined && !priestButton.activeSelf) priestButton.SetActive(true);

        if (GameObject.FindGameObjectWithTag("JournalPage"))
        {
            if (!journalIndicator.activeSelf) journalIndicator.SetActive(true);
        }
        else if (journalIndicator.activeSelf) journalIndicator.SetActive(false);

        if (SceneManager.GetActiveScene().name == "Hub" && goHomeButton.activeSelf) goHomeButton.SetActive(false);
        else if (SceneManager.GetActiveScene().name != "Hub" && !goHomeButton.activeSelf) goHomeButton.SetActive(true);
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

    public void ShowNextLevelWindow() 
    {
        nextLevelWindow.SetActive(true);
        SetTimeFlowing(false);
    }

    public void NextLevel() 
    {
        try
        {
            //reset rotation
            GameObject.FindGameObjectWithTag("Player").transform.rotation = new Quaternion(0, 0, 0, 0);
            //hide window
            nextLevelWindow.SetActive(false);
            //if its not the hub
            if (SceneManager.GetActiveScene().name != "Hub")
            {
                //increment current level
                GameData.NextLevel(int.Parse(SceneManager.GetActiveScene().name.ToString()) + 1);
            }
            //save
            GameData.Save();
            //load scene
            SceneManager.LoadScene(GameData.currentLevel.ToString());
        }
        catch
        {
            Debug.Log("Invalid Scene");
        }
    }

    public void GoHome() 
    {
        nextLevelWindow.SetActive(false);
        if (SceneManager.GetActiveScene().name != "Hub")
        {
            //increment current level
            GameData.NextLevel(int.Parse(SceneManager.GetActiveScene().name.ToString()) + 1);
            Debug.Log("Incremented");
            //reset rotation
            GameObject.FindGameObjectWithTag("Player").transform.rotation = new Quaternion(0, 0, 0, 0);
            //hide window
            nextLevelWindow.SetActive(false);
            //save
            GameData.Save();
            //load hub
            SceneManager.LoadScene("Hub");
        }
    }

    public void SetTimeFlowing(bool set) 
    {
        if (set) Time.timeScale = 1;
        else Time.timeScale = 0;
    }
}