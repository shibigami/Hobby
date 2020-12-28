using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int currentLevel { get; private set; }
    public static int gold { get; private set; }
    public static int lives { get; private set; }
    public static int[] collectedPages { get; private set; }
    public static bool sideKickJoined { get; private set; }


    public static void init() 
    {
        currentLevel = 0;
        gold = 0;
        lives = 0;
        collectedPages = new int[Journal.numberOfPages];
        //collected pages int array fill with 0's
        //it's value changes to 1 when the user has found the corresponding page
        for (int i = 0; i < Journal.numberOfPages; i++) collectedPages[i] = 0;

        sideKickJoined = false;
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

    public static void SideKickJoins() 
    {
        sideKickJoined = true;
    }
}
