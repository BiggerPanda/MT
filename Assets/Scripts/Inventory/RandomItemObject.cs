using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomItemObject : ItemObject
{
    [SerializeField] protected List<InventoryItemData> _randomItemDatas;

    public override void PickUp()
    {
        if (_inventoryController != null)
        {
            _inventoryController.CreateRandomItem(_randomItemDatas);
            Destroy(gameObject);
        }
    }


}
