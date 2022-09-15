using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace MT.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private RectTransform _canvasRectTransform;
        public GridItem _currentItemGrid;
        
        private InventorySystemManager _inventorySystemManager;
        private Vector2 _mousePosition;
        private bool _isOpened = false;
        private InventoryItem _selectedItem;
        private RectTransform _selectedItemRectTransform;
        
        
        

        [Inject]
        public virtual void Construct(InventorySystemManager inventorySystemManager)
        {
            _inventorySystemManager = inventorySystemManager;
        }

        public virtual void ReadInput(Vector2 input)
        {
            _mousePosition = input;
        }

        protected virtual void Awake()
        {
            _canvasRectTransform = _currentItemGrid.GetComponent<RectTransform>();
        }

        public virtual void ToggleInventory()
        {
            _isOpened = !_isOpened;
            Cursor.lockState = _isOpened ? CursorLockMode.Confined : CursorLockMode.Locked;
        }

        public virtual void OnMouseClick()
        {
            if (_currentItemGrid == null || !_currentItemGrid.gameObject.activeSelf)
            {
                return;
            }

            Vector2Int position = _currentItemGrid.GetGridPosition(_mousePosition);
            
            if (_selectedItem == null)
            {
                PickUpItemOnGrid(position);
            }
            else
            {
                PlaceItem(position);
            }
        }

        protected virtual void PickUpItemOnGrid(Vector2Int position)
        {
            _selectedItem = _currentItemGrid.PickItem(position);
            if (_selectedItem != null)
            {
                _selectedItemRectTransform = _selectedItem.GetComponent<RectTransform>();
            }
        }

        protected virtual void PlaceItem(Vector2Int position)
        {
            _currentItemGrid.AddItem(_selectedItem, position);
            _selectedItem = null;
        }


        // Update is called once per frame
        protected virtual void Update()
        {
            ItemIconDragHandle();
        }

        protected virtual void ItemIconDragHandle()
        {
            if (_selectedItem != null)
            {
                _selectedItemRectTransform.position = _mousePosition;
            }
        }

        public virtual void AddItemToInventory(InventoryItemData itemData)
        {
            _inventorySystemManager.AddItem(itemData);
        }
        
        public virtual void CreateRandomItem(List<InventoryItemData> randomItemDatas)
        {
            InventoryItem inventoryItem = Instantiate(_itemPrefab).GetComponent<InventoryItem>();
            _selectedItem = inventoryItem;
            
            _selectedItemRectTransform = inventoryItem.GetComponent<RectTransform>();
            _selectedItemRectTransform.SetParent(_canvasRectTransform);
            
            
            inventoryItem.Setup(randomItemDatas[Random.Range(0, randomItemDatas.Count - 1)]);
        }
    }
}