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
        if (unlockSideKick.GetComponent<Toggle>().isOn) GameData.SideKickJoins();
        if (unlockMage.GetComponent<Toggle>().isOn) GameData.MageJoins();
        if (unlockPriest.GetComponent<Toggle>().isOn) GameData.PriestJoins();
    }
}
