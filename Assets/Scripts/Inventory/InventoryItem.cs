using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventoryItem 
{
    public InventoryItemData _data;
    public int _amount;

    public InventoryItem(InventoryItemData data, int amount)
    {
        _data = data;
        _amount = amount;
    }
    public InventoryItem(InventoryItemData data)
    {
        _data = data;
        AddItem();
    }
    public void AddItem()
    {
        _amount++;
    }
    public void RemoveItem()
    {
        _amount--;
        if (_amount < 0)
        {
            _amount = 0;
        }
    }
    public void AddItem(int amount)
    {
        _amount += amount;
    }
    public void RemoveItem(int amount)
    {
        _amount -= amount;
        if (_amount < 0)
        {
            _amount = 0;
        }
    }

}
