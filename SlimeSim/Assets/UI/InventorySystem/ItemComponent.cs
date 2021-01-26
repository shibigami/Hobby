using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemComponent : MonoBehaviour
{
    public int ID;
    private UIInventorySystem uiInventorySystem;

    private void Start()
    {
        uiInventorySystem = GameObject.FindGameObjectWithTag("Inventory").GetComponent<UIInventorySystem>();
    }

    public void SetDetails() 
    {
        uiInventorySystem.SetItem(ID);
    }
}
