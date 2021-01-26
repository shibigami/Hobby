using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string name { get; private set; }
    public int ID { get; private set; }
    public string description { get; private set; }
    public bool usable { get; private set; }
    private Action action;

    public Item(string _name, string _description)
    {
        name = _name;
        description = _description;
        usable = false;
    }
    public Item(string _name, string _description,bool _usable)
    {
        name = _name;
        description = _description;
        usable = _usable;
    }

    public void setName(string _name) 
    {
        name = _name;
    }
    public void setDescription(string _description) 
    {
        description = _description;
    }
    public void setUsable(Action useAction) 
    {
        usable = true;

        action = useAction;
    }

    public bool useItem() 
    {
        if (usable)
        {
            action.Invoke();
            InventorySystem.TakeItem(ID);
            return true;
        }
        else return false;
    }
}
