using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventorySystemManager
{
    void AddItem(InventoryItemData itemData);
    void RemoveItem(InventoryItemData itemData);
}

public class InventorySystemManager : MonoBehaviour,IInventorySystemManager
{
    private Dictionary<InventoryItemData, InventoryItem> _inventoryItemDataDictionary;
    public  List<InventoryItem> _inventoryItems;

    void Awake()
    {
        _inventoryItemDataDictionary = new Dictionary<InventoryItemData, InventoryItem>();
        _inventoryItems = new List<InventoryItem>();
    }
    public void AddItem(InventoryItemData data)
    {
        if(_inventoryItemDataDictionary.TryGetValue(data , out InventoryItem existingData))
        {
            existingData.AddItem();
        }
        else
        {
            InventoryItem item = new InventoryItem(data);
            _inventoryItemDataDictionary.Add(data, item);
            _inventoryItems.Add(item);
        }

    }
    public void RemoveItem(InventoryItemData item)
    {
        if(_inventoryItemDataDictionary.TryGetValue(item, out InventoryItem existingItem))
        {
            existingItem.RemoveItem();
            if(existingItem._amount == 0)
            {
                _inventoryItemDataDictionary.Remove(item);
                _inventoryItems.Remove(existingItem);
            }
        }
    }
}
