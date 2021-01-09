using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingUnit : MonoBehaviour
{
    public Toggle dropGold;
    public Toggle unlockSideKick;
    public Toggle unlockMage;
    public Toggle unlockPriest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dropGold.GetComponent<Toggle>().isOn) GameObject.FindGameObjectWithTag("UI").GetComponent<Outputs>().UnlockDropGold();
        if (unlockSideKick.GetComponent<Toggle>().isOn)
        {
            if(!GameData.sideKickJoined) Journal.UpdateSideKickBranch();
            GameData.SideKickJoins();
        }
        if (unlockMage.GetComponent<Toggle>().isOn && !GameData.mageJoined)
        {
            Journal.UpdateSideKickBranch();
            for (int i = 0; i < 25; i++) GameData.AddPageToCollection(i);
            GameData.MageJoins();
        }
        if (unlockPriest.GetComponent<Toggle>().isOn && !GameData.priestJoined)
        {
            Journal.UpdateSideKickBranch();
            for (int i = 0; i < 25; i++) GameData.AddPageToCollection(i);
            GameData.PriestJoins();
        }
    }
}
