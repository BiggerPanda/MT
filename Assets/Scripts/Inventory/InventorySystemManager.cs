using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventorySystemManager
{
    void AddItem(InventoryItemData itemData);
    void RemoveItem(InventoryItem itemData);
}

public class InventorySystemManager : MonoBehaviour,IInventorySystemManager
{
    private Dictionary<InventoryItem, InventoryItemData> _inventoryItemDataDictionary;
    public  List<InventoryItem> _inventoryItems;

    void Awake()
    {
        _inventoryItemDataDictionary = new Dictionary<InventoryItem, InventoryItemData>();
        _inventoryItems = new List<InventoryItem>();
    }
    public void AddItem(InventoryItemData data)
    {
        InventoryItem item = new InventoryItem(data);
        _inventoryItemDataDictionary.Add(item, data);
        _inventoryItems.Add(item);
    }
    public void RemoveItem(InventoryItem item)
    {
        _inventoryItemDataDictionary.Remove(item);
        _inventoryItems.Remove(item);
    }
}
