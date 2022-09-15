using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MT.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        private InventorySystemManager _inventorySystemManager;
        public GridItem _currentItemGrid;
        private Vector2 _mousePosition;
        bool isOpened = false;

        [Inject]
        public void Construct(InventorySystemManager inventorySystemManager)
        {
            _inventorySystemManager = inventorySystemManager;
        }

        public void ReadInput(Vector2 input)
        {
            _mousePosition = input;
        }
        // Start is called before the first frame update

        void Start()
        {

        }

        public void ToggleInventory()
        {
            isOpened = !isOpened;
            if (isOpened)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        public void OnMouseClick()
        {
            if (_currentItemGrid == null)
            {
                return;
            }
            if (!_currentItemGrid.gameObject.activeSelf)
            {
                return;
            }
            Debug.Log(_currentItemGrid.GetGridPosition(_mousePosition));
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}