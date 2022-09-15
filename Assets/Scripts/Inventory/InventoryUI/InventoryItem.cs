using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MT.Inventory
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] protected Image _objectImage;
        protected InventoryItemData _itemData;


        public virtual void Setup(InventoryItemData data)
        {
            _itemData = data;
            _objectImage.sprite = _itemData.itemIcon;
        }
    }
}