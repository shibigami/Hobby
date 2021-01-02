using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public string name { get; private set; }
    public int level { get; private set; }
    public int maxLevel { get; private set; }
    public int upgradeCost { get; private set; }
    public float powerPerLevel { get; private set; }
    public string description { get; private set; }
    public float coolDown { get; private set; }
    private float nextUseTimeStamp;

    public Spell(string spellName)
    {
        name = spellName;
        level = 0;
        maxLevel = 10;
        upgradeCost = 0;
        powerPerLevel = 0;
        coolDown = 0;
    }
    public Spell(string spellName,int initialLevel)
    {
        name = spellName;
        level = initialLevel;
        maxLevel = 10;
        upgradeCost = 0;
        powerPerLevel = 0;
        coolDown = 0;
    }
    public Spell(string spellName,int initialLevel,int costToUpgrade)
    {
        name = spellName;
        level = initialLevel;
        maxLevel = 10;
        upgradeCost = costToUpgrade;
        powerPerLevel = 0;
        coolDown = 0;
    }
    public Spell(string spellName, int initialLevel, int costToUpgrade, float powerIncrementPerLevel)
    {
        name = spellName;
        level = initialLevel;
        maxLevel = 10;
        upgradeCost = costToUpgrade;
        powerPerLevel = powerIncrementPerLevel;
        coolDown = 0;
    }
    public Spell(string spellName, int initialLevel, int maximumLevel, int costToUpgrade, float powerIncrementPerLevel)
    {
        name = spellName;
        level = initialLevel;
        maxLevel = maximumLevel;
        upgradeCost = costToUpgrade;
        powerPerLevel = powerIncrementPerLevel;
        coolDown = 0;
    }
    public Spell(string spellName, int initialLevel, int maximumLevel, int costToUpgrade, float powerIncrementPerLevel,float spellCooldown)
    {
        name = spellName;
        level = initialLevel;
        maxLevel = maximumLevel;
        upgradeCost = costToUpgrade;
        powerPerLevel = powerIncrementPerLevel;
        coolDown = spellCooldown;
    }

    public void SetDescription(string spellDescription) 
    {
        description = spellDescription;
    }

    public float GetPower()
    {
        return powerPerLevel * level;
    }

    public float UseActiveSpell() 
    {
        nextUseTimeStamp = Time.time + coolDown;
        return GetPower();
    }

    public float GetCurrentCooldown()
    {
        return nextUseTimeStamp - Time.time;
    }

    public bool IncreaseSpellLevel(int amount) 
    {
        if (level + amount <= maxLevel)
        {
            level += amount;
            return true;
        }
        else return false;
    }
}