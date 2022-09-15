using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace MT.Inventory
{
    public class GridItem : MonoBehaviour
    {
        const float tileSizeX = 64f;
        const float tileSizeY = 64f;

        [SerializeField] private Image inventory;
        public int _inventoryXSize = 5;
        public int _inventoryYSize = 5;
        private Vector2 _positionOnGrid;
        private Vector2Int _tileGridPosition;
        private InventorySystemManager _inventorySystemManager;
        private InventoryController _inventoryController;
        private InventoryItem[,] _inventoryItem;

        [Inject]
        public void Construct(InventorySystemManager inventorySystemManager, InventoryController inventoryController)
        {
            _inventorySystemManager = inventorySystemManager;
            _inventoryController = inventoryController;
        }

        // Start is called before the first frame update
        private void Start()
        {
            Init(_inventoryXSize, _inventoryYSize);
        }

        private void Init(int width , int height)
        {
            _inventoryItem = new InventoryItem[width, height];
            inventory.rectTransform.sizeDelta = new Vector2(width * tileSizeX, height * tileSizeY);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public Vector2Int GetGridPosition(Vector2 position)
        {
            _positionOnGrid.x = position.x - inventory.rectTransform.position.x;
            _positionOnGrid.y = inventory.rectTransform.position.y - position.y;
            _tileGridPosition.x = (int)(_positionOnGrid.x / tileSizeX);
            _tileGridPosition.y = (int)(_positionOnGrid.y / tileSizeY);

            return _tileGridPosition;
        }

        public void AddItem(InventoryItem item, Vector2Int position)
        {
            RectTransform itemTransform = item.GetComponent<RectTransform>();
            itemTransform.SetParent(inventory.transform);
            _inventoryItem[position.x, position.y] = item;
            
            float positionX = (position.x * tileSizeX )+ tileSizeX / 2;
            float positionY = (position.y * tileSizeY )+ tileSizeY / 2;
            Vector2 itemPosition = new Vector2(positionX,-positionY);
            itemTransform.localPosition = itemPosition;
        }

        public InventoryItem PickItem(Vector2Int position)
        {
            InventoryItem item = _inventoryItem[position.x, position.y];
            _inventoryItem[position.x, position.y] = null;
            return item;
        }

        public bool IsEmpty(int x, int y)
        {
            return _inventoryItem[x, y] == null;
        }
    }
}
