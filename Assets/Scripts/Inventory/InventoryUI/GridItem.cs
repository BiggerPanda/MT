using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace MT.Inventory
{
    public class GridItem : MonoBehaviour
    {
        public const float TILE_SIZE_X = 64f;
        public const float TILE_SIZE_Y = 64f;

        [SerializeField] private Image inventory;
        public int _inventoryXSize;
        public int _inventoryYSize;
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

        private void Init(int width, int height)
        {
            _inventoryItem = new InventoryItem[width, height];
            inventory.rectTransform.sizeDelta = new Vector2(width * TILE_SIZE_X, height * TILE_SIZE_Y);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public Vector2Int GetGridPosition(Vector2 position)
        {
            _positionOnGrid.x = position.x - inventory.rectTransform.position.x;
            _positionOnGrid.y = inventory.rectTransform.position.y - position.y;
            _tileGridPosition.x = (int)(_positionOnGrid.x / TILE_SIZE_X);
            _tileGridPosition.y = (int)(_positionOnGrid.y / TILE_SIZE_Y);

            return _tileGridPosition;
        }

        public void AddItem(InventoryItem item, Vector2Int position)
        {
            RectTransform itemTransform = item.GetComponent<RectTransform>();
            itemTransform.SetParent(inventory.transform);
            for (int x = 0; x < item.ItemData.width; x++)
            {
                for (int y = 0; y < item.ItemData.height; y++)
                {
                    _inventoryItem[position.x + x, position.y + y] = item;
                }
            }

            item.PositionOnGrid = position;

            float positionX = (position.x * TILE_SIZE_X) + TILE_SIZE_X * item.ItemData.width / 2;
            float positionY = (position.y * TILE_SIZE_Y) + TILE_SIZE_Y * item.ItemData.height / 2;
            Vector2 itemPosition = new Vector2(positionX, -positionY);
            itemTransform.localPosition = itemPosition;
        }

        public InventoryItem PickItem(Vector2Int position)
        {
            InventoryItem item = _inventoryItem[position.x, position.y];
            for (int x = 0; x < item.ItemData.width; x++)
            {
                for (int y = 0; y < item.ItemData.height; y++)
                {
                    _inventoryItem[(int)item.PositionOnGrid.x + x, (int)item.PositionOnGrid.y + y] = null;
                }
            }
            _inventoryItem[position.x, position.y] = null;
            return item;
        }

        public bool IsEmpty(int x, int y)
        {
            return _inventoryItem[x, y] == null;
        }

        private bool PositionCheck(Vector2 position)
        {
            if (position.x < 0 || position.y < 0)
            {
                return false;
            }

            if (position.x > _inventoryXSize || position.y >_inventoryYSize)
            {
                return false;
            }

            return true;
        }

        private bool BoundryCheck(Vector2 position, Vector2 size)
        {
            
            return true;
        }
    }
}
