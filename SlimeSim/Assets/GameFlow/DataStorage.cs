using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class DataStorage
{
    string path = Application.persistentDataPath + "/GameData.xml";

    //GameData info
    public int currentLevel;
    public int gold;
    public int lives;
    public int[] collectedPages;
    public bool goldDropUnlocked;
    public bool inventoryUnlocked;
    public bool sideKickJoined;
    public bool mageJoined;
    public bool priestJoined;
    public bool fakeWallJoined;
    //Magic info
    public Spell[] mageSpells;
    public Spell[] priestSpells;
    //InventoryStorage
    public int[] itemAmounts;

    //Journal? Journal pages unlocked is stored in GameData
    //Journal only needs to be updated on load in GameData


    public void Load()
    {
        var serializer = new XmlSerializer(typeof(DataStorage));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            DataStorage container = serializer.Deserialize(stream) as DataStorage;
            stream.Close();
            SetDataToLoad(container);
        }
    }

    public void Save()
    {
        SetDataToSave();
        var serializer = new XmlSerializer(typeof(DataStorage));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
            stream.Close();
        }

        Debug.Log("Saved to " + path);
    }

    private void SetDataToSave()
    {
        currentLevel = GameData.currentLevel;
        gold = GameData.gold;
        lives = GameData.lives;
        collectedPages = new int[GameData.collectedPages.Length];
        collectedPages = GameData.collectedPages;
        goldDropUnlocked = GameData.goldDropUnlocked;
        inventoryUnlocked = GameData.inventoryUnlocked;
        sideKickJoined = GameData.sideKickJoined;
        mageJoined = GameData.mageJoined;
        priestJoined = GameData.priestJoined;
        fakeWallJoined = GameData.fakeWallJoined;
        mageSpells = Magic.mageSpells;
        priestSpells = Magic.priestSpells;
        itemAmounts = new int[InventorySystem.itemAmount.Length];
        itemAmounts = InventorySystem.itemAmount;
    }

    private void SetDataToLoad(DataStorage tempStorage)
    {
        currentLevel = tempStorage.currentLevel;
        gold = tempStorage.gold;
        lives = tempStorage.lives;
        collectedPages = new int[tempStorage.collectedPages.Length];
        collectedPages = tempStorage.collectedPages;
        goldDropUnlocked = tempStorage.goldDropUnlocked;
        inventoryUnlocked = tempStorage.inventoryUnlocked;
        sideKickJoined = tempStorage.sideKickJoined;
        mageJoined = tempStorage.mageJoined;
        priestJoined = tempStorage.priestJoined;
        fakeWallJoined = tempStorage.fakeWallJoined;
        //extended array initialization in case of failure from poor initialization
        mageSpells = new Spell[tempStorage.mageSpells.Length];
        for (int i = 0; i < mageSpells.Length; i++)
        {
            mageSpells[i] = new Spell(tempStorage.mageSpells[i].spellName,
                tempStorage.mageSpells[i].level,
                tempStorage.mageSpells[i].maxLevel,
                tempStorage.mageSpells[i].upgradeCost,
                tempStorage.mageSpells[i].powerPerLevel,
                tempStorage.mageSpells[i].coolDown);
            mageSpells[i].description = tempStorage.mageSpells[i].description;
        }
        priestSpells = new Spell[tempStorage.priestSpells.Length];
        for (int i = 0; i < priestSpells.Length; i++)
        {
            priestSpells[i] = new Spell(tempStorage.priestSpells[i].spellName,
                tempStorage.priestSpells[i].level,
                tempStorage.priestSpells[i].maxLevel,
                tempStorage.priestSpells[i].upgradeCost,
                tempStorage.priestSpells[i].powerPerLevel,
                tempStorage.priestSpells[i].coolDown);
            priestSpells[i].description = tempStorage.priestSpells[i].description;
        }
        //inventory
        itemAmounts = new int[InventorySystem.itemAmount.Length];
        itemAmounts = tempStorage.itemAmounts;
    }

    public bool CheckFileExists() 
    {
        if (File.Exists(path)) return true;
        else return false;
    }
}
