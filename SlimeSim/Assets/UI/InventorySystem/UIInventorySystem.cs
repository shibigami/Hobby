using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySystem : MonoBehaviour
{
    private GameObject player;

    public Text nameText,infoText,amountText;
    public GameObject useButton, deleteButton;

    //collection array of all item types in inventorySystem
    public GameObject[] itemList;

    //collection of prefabs for spawnable items
    public GameObject[] spawnableItemPrefabs;

    private int IDSet;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //deactivate buttons
        useButton.SetActive(false);
        deleteButton.SetActive(false);

        //set icons
        UpdateItemAmounts();

        //clear item selection
        ClearItemSelected();
    }

    private void FixedUpdate()
    {
        if (player.GetComponent<Rigidbody2D>().velocity!=Vector2.zero) gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void UpdateItemAmounts()
    {
        if (itemList.Length <= InventorySystem.itemAmount.Length)
        {
            for (int i = 0; i < itemList.Length; i++)
            {
                if (InventorySystem.itemAmount[i] <= 0) itemList[i].SetActive(false);
                else itemList[i].SetActive(true);
            }
        }
    }

    public void SetItem(int _id)
    {
        //used for functions
        IDSet = _id;

        //set info
        nameText.text = string.Format("{0}",InventorySystem.itemList[IDSet].name);
        infoText.text = InventorySystem.itemList[IDSet].description;
        amountText.text = string.Format("Held amount: {0}",InventorySystem.itemAmount[IDSet]);
        //set use button
        if (InventorySystem.itemList[IDSet].usable)
        {
            //set use
            SetItemUse();
            //set ui
            if (!useButton.activeSelf) useButton.SetActive(true);
            useButton.GetComponent<Button>().onClick.RemoveAllListeners();
            useButton.GetComponent<Button>().onClick.AddListener(UseItem);
        }
        else
        {
            if (useButton.activeSelf) useButton.SetActive(false);
        }
        //set delete button
        if (!deleteButton.activeSelf) deleteButton.SetActive(true);
        deleteButton.GetComponent<Button>().onClick.RemoveAllListeners();
        deleteButton.GetComponent<Button>().onClick.AddListener(DeleteItem);
    }

    public void ClearItemSelected() 
    {
        nameText.text = "";
        infoText.text = "";
        amountText.text = "";
        useButton.SetActive(false);
        deleteButton.SetActive(false);
    }

    public void UseItem() 
    {
        InventorySystem.itemList[IDSet].useItem();

        //update icons
        UpdateItemAmounts();

        //clear item selection
        ClearItemSelected();

        //and close
        gameObject.SetActive(false);
    }

    public void AddItem(int id) 
    {
        InventorySystem.AddItem(id);
    }
    public void DeleteItem()
    {
        InventorySystem.TakeItem(IDSet, 1);
        //update icons
        UpdateItemAmounts();
        //update quantity held
        amountText.text = string.Format("Held amount: {0}", InventorySystem.itemAmount[IDSet]);
        //clear selection if dont have items anymore
        if (InventorySystem.itemAmount[IDSet] <= 0) ClearItemSelected();
    }

    private void SetItemUse() 
    {
        //create the corresponding item according to list of prefabs and
        //item ids set in InventorySystem item list
        switch (IDSet)
        {
            //item id 0=seed of light
            case 0: { InventorySystem.itemList[IDSet].setUsable(SpawnSeedOfLight); break; }
            //item id 1=fruit of light
            case 1: { InventorySystem.itemList[IDSet].setUsable(SpawnFruitOfLight); break; }
        }
    }

    private void SpawnSeedOfLight() 
    {
        Instantiate(spawnableItemPrefabs[0], player.transform.position, spawnableItemPrefabs[0].transform.rotation);
    }
    private void SpawnFruitOfLight()
    {
        Instantiate(spawnableItemPrefabs[1], player.transform.position, spawnableItemPrefabs[1].transform.rotation);
    }
}
