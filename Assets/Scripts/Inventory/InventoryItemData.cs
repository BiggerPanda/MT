using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item Data", menuName = "Inventory/Inventory Item Data")]
public class InventoryItemData : ScriptableObject
{
    public int width;
    public int height;
    public string itemID;
    public string itemName;
    public Sprite itemIcon;
    public GameObject itemPrefab;
    public bool stackable;
    [ShowIf("stackable")]
    public int maxStackAmount;    
}

