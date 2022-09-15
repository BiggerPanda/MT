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
        protected Vector2 _size;

        public virtual void Setup(InventoryItemData data)
        {
            _itemData = data;
            _objectImage.sprite = _itemData.itemIcon;
            _size = new Vector2(_itemData.width * GridItem.TILE_SIZE_X, _itemData.height * GridItem.TILE_SIZE_Y);
            GetComponent<RectTransform>().sizeDelta = _size;
        }
    }
}