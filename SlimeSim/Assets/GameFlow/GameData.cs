using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int currentLevel { get; private set; }
    public static int gold { get; private set; }
    public static int lives { get; private set; }
    public static int[] collectedPages { get; private set; }
    public static bool goldDropUnlocked { get; private set; }
    public static bool sideKickJoined { get; private set; }
    public static bool mageJoined { get; private set; }
    public static bool priestJoined { get; private set; }


    public static void init() 
    {
        currentLevel = 0;
        gold = 100000;
        lives = 0;

        Journal.init();
        collectedPages = new int[Journal.numberOfPages];
        //collected pages int array fill with 0's
        //it's value changes to 1 when the user has found the corresponding page
        for (int i = 0; i < Journal.numberOfPages; i++) collectedPages[i] = 0;

        goldDropUnlocked = false;
        sideKickJoined = false;
        mageJoined = false;
        priestJoined = false;

        Magic.init();
    }

    public static void NextLevel() 
    {
        currentLevel++;
    }
    public static void AddGold(int amount) 
    {
        gold += amount;
    }
    public static void Die() 
    {
        lives++;
    }

    public static void AddPageToCollection(int index) 
    {
        collectedPages[index] = 1;
    }

    public static void UnlockGoldDrop()
    {
        goldDropUnlocked = true;
    }

    public static void SideKickJoins() 
    {
        sideKickJoined = true;
    }

    public static void MageJoins() 
    {
        mageJoined = true;
        Magic.SetMagicTypeChosen(Magic.MagicType.Mage);
        Journal.UpdateMagePriestBranch();
    }

    public static void PriestJoins() 
    {
        priestJoined = true;
        Magic.SetMagicTypeChosen(Magic.MagicType.Priest);
        Journal.UpdateMagePriestBranch();
    }
}
