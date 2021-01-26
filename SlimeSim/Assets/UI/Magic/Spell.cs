using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public string spellName;
    public int level;
    public int maxLevel;
    public int upgradeCost;
    public float powerPerLevel;
    public string description;
    public float coolDown;
    private float nextUseTimeStamp;

    public Spell() 
    {
        spellName = "";
        level = 0;
        maxLevel = 0;
        upgradeCost = 0;
        powerPerLevel = 0;
        coolDown = 0;
        description = "";
    }
    public Spell(string spellName)
    {
        this.spellName = spellName;
        level = 0;
        maxLevel = 10;
        upgradeCost = 0;
        powerPerLevel = 0;
        coolDown = 0;
    }
    public Spell(string spellName,int initialLevel)
    {
        this.spellName = spellName;
        level = initialLevel;
        maxLevel = 10;
        upgradeCost = 0;
        powerPerLevel = 0;
        coolDown = 0;
    }
    public Spell(string spellName,int initialLevel,int costToUpgrade)
    {
        this.spellName = spellName;
        level = initialLevel;
        maxLevel = 10;
        upgradeCost = costToUpgrade;
        powerPerLevel = 0;
        coolDown = 0;
    }
    public Spell(string spellName, int initialLevel, int costToUpgrade, float powerIncrementPerLevel)
    {
        this.spellName = spellName;
        level = initialLevel;
        maxLevel = 10;
        upgradeCost = costToUpgrade;
        powerPerLevel = powerIncrementPerLevel;
        coolDown = 0;
    }
    public Spell(string spellName, int initialLevel, int maximumLevel, int costToUpgrade, float powerIncrementPerLevel)
    {
        this.spellName = spellName;
        level = initialLevel;
        maxLevel = maximumLevel;
        upgradeCost = costToUpgrade;
        powerPerLevel = powerIncrementPerLevel;
        coolDown = 0;
    }
    public Spell(string spellName, int initialLevel, int maximumLevel, int costToUpgrade, float powerIncrementPerLevel,float spellCooldown)
    {
        this.spellName = spellName;
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

    public void SetRemainingCoolDown(int remainingTimeSeconds) 
    {
        nextUseTimeStamp = Time.time + remainingTimeSeconds;
    }
}