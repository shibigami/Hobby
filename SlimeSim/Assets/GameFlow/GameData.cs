using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int currentLevel { get; private set; }
    public static int gold { get; private set; }
    public static int lives { get; private set; }


    public static void init() 
    {
        currentLevel = 0;
        gold = 0;
        lives = 0;
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
}
