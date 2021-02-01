using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HubData 
{
    public static int wallCoins { get; private set; }
    public static bool wallHouseBuilt { get; private set; }

    public static void init() 
    {
        wallCoins = 0;
        wallHouseBuilt = false;
    }

    public static void LoadHubData() 
    {
        wallCoins = GameData.dataStorage.wallCoins;
        wallHouseBuilt = GameData.dataStorage.wallHouseBuilt;
    }

    public static void AddWallCoins()
    {
        wallCoins++;
    }
    public static void AddWallCoins(int amount)
    {
        wallCoins+=amount;
    }

    public static int GetHouseBricksBuilt() 
    {
        return Mathf.FloorToInt(wallCoins/20);
    }

    public static void WallHouseFinalized() 
    {
        wallHouseBuilt = true;
    }
}
