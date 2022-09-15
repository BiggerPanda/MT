using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.EventSystems;

namespace MT.Inventory
{
    [RequireComponent(typeof(GridItem))]
    public class InventoryInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private InventoryController _inventoryController;
        private GridItem _gridItem;

        [Inject]
        public void Construct(InventoryController inventoryController)
        {
            _inventoryController = inventoryController;
        }

        // Start is called before the first frame update
        void Start()
        {
            _gridItem = GetComponent<GridItem>();
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            _inventoryController._currentItemGrid = _gridItem;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _inventoryController._currentItemGrid = null;
        }
    }
}