using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventorySystem
{
    public static Item[] itemList { get; private set; }
    public static int[] itemAmount { get; private set; }

    public static void init()
    {
        //initialize item list
        itemList = new Item[2];
        itemList[0] = new Item("Seed of Light", "The flower that comes from this seed " +
            "allows one to see it's surroundings.", true);
        itemList[1] = new Item("Fruit of Light", "This is a fruit. " +
            "Eating it increases vision radius for some time.", true);

        //initialize item amounts
        //index of item in item list is the ID and the same index as the item amount array
        itemAmount = new int[itemList.Length];
        for (int i = 0; i < itemList.Length; i++)
        {
            itemAmount[i] = 0;
        }
    }

    public static Item getItem(int id)
    {
        return itemList[id];
    }
    public static void AddItem(int id)
    {
        itemAmount[id]++;
    }
    public static void AddItem(int id, int amount)
    {
        itemAmount[id] += amount;
    }
    public static void TakeItem(int id)
    {
        itemAmount[id]--;
    }
    public static void TakeItem(int id, int amount)
    {
        itemAmount[id] -= amount;
    }

    public static void OverwriteItemAmounts(int[] newItemAmounts) 
    {
        if (newItemAmounts.Length < itemList.Length)
        {
            for (int i = 0; i < itemAmount.Length; i++)
            {
                if (i < newItemAmounts.Length) itemAmount[i] = newItemAmounts[i];
                else itemAmount[i] = 0;
            }
        }
        else if (newItemAmounts.Length > itemList.Length)
        {
            for (int i = 0; i < itemAmount.Length; i++)
            {
                itemAmount[i] = newItemAmounts[i];
            }
        }
        else itemAmount = newItemAmounts;
    }
}