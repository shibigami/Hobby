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
    public bool journalUnlocked { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < journalPageButtons.Length; i++) journalPageButtons[i].SetActive(false);

        journalButton.SetActive(false);

        DontDestroyOnLoad(gameObject);

        InvokeRepeating("UpdateUI", 0, 0.5f);
    }

    void UpdateUI()
    {
        goldText.text = GameData.gold.ToString();
        livesText.text = GameData.lives.ToString();

        if (journalUnlocked && !journalButton.activeSelf) journalButton.SetActive(true);
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
}
