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
    public static bool inventoryUnlocked { get; private set; }
    public static bool sideKickJoined { get; private set; }
    public static bool mageJoined { get; private set; }
    public static bool priestJoined { get; private set; }
    public static bool fakeWallJoined { get; private set; }

    public static DataStorage dataStorage { get; private set; }

    public static void init()
    {
        dataStorage = new DataStorage();

        if (!dataStorage.CheckFileExists())
        {
            currentLevel = 0;
            gold = 0;
            lives = 0;

            goldDropUnlocked = false;
            inventoryUnlocked = false;
            sideKickJoined = false;
            mageJoined = false;
            priestJoined = false;
            fakeWallJoined = false;

            Journal.SetInitialStory();
            collectedPages = new int[Journal.numberOfPages];
            //collected pages int array fill with 0's
            //it's value changes to 1 when the user has found the corresponding page
            for (int i = 0; i < Journal.numberOfPages; i++) collectedPages[i] = 0;

            Magic.init();

            InventorySystem.init();

        }
        else
        {

            ReLoadData();
        }
    }

    public static void ReLoadData()
    {
        //set initial story
        Journal.SetInitialStory();
        //initialization
        collectedPages = new int[Journal.numberOfPages];
        //collected pages int array fill with 0's
        //it's value changes to 1 when the user has found the corresponding page
        for (int i = 0; i < Journal.numberOfPages; i++) collectedPages[i] = 0;

        Magic.init();

        InventorySystem.init();
        //load data into data storage instance
        dataStorage.Load();

        //load data from instance storage into game data
        LoadFromDataStorage();

        //update choice branches
        Journal.UpdateSideKickBranch();
        //magic branches
        if (mageJoined)
        {
            Journal.UpdateMagePriestBranch();
            Magic.SetMagicTypeChosen(Magic.MagicType.Mage);
        }
        else if (priestJoined)
        {
            Journal.UpdateMagePriestBranch();
            Magic.SetMagicTypeChosen(Magic.MagicType.Priest);
        }
        else Magic.SetMagicTypeChosen(Magic.MagicType.None);

        Magic.LoadSpells();

        //inventory
        InventorySystem.OverwriteItemAmounts(dataStorage.itemAmounts);
    }

    private static void LoadFromDataStorage()
    {
        currentLevel = dataStorage.currentLevel;
        gold = dataStorage.gold;
        lives = dataStorage.lives;
        for (int i = 0; i < collectedPages.Length; i++)
        {
            collectedPages[i] = dataStorage.collectedPages[i];
        }
        goldDropUnlocked = dataStorage.goldDropUnlocked;
        inventoryUnlocked = dataStorage.inventoryUnlocked;
        sideKickJoined = dataStorage.sideKickJoined;
        mageJoined = dataStorage.mageJoined;
        priestJoined = dataStorage.priestJoined;
        fakeWallJoined = dataStorage.fakeWallJoined;
    }

    public static void Save() 
    {
        dataStorage.Save();
    }

    public static void NextLevel(int level) 
    {
        currentLevel = level;
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

    public static void UnlockInventory() 
    {
        inventoryUnlocked = true;
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

    public static void FakeWallJoins() 
    {
        fakeWallJoined = true;
    }
}
