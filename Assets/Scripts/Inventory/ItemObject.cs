using MT.Inventory;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
public class ItemObject : MonoBehaviour
{
    [Expandable] [SerializeField] private InventoryItemData _itemData;

    [SerializeField] protected Collider _collider;
    [SerializeField] protected Canvas _canvas;
    
    protected InventoryController _inventoryController;


    [Inject]
    public virtual void Construct(InventorySystemManager inventorySystemManager , InventoryController inventoryController)
    {
        _inventoryController = inventoryController;
    }
    

    public virtual void PickUp()
    {
        if(_inventoryController != null)
        {
            _inventoryController.AddItemToInventory(_itemData);
            Destroy(gameObject);
        }
    }
    protected  virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _canvas.gameObject.SetActive(true);
        }
    }
    protected virtual void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Quaternion temp = Quaternion.LookRotation(transform.position - other.transform.position);
            _canvas.transform.rotation = new Quaternion(0, -temp.y, 0, -temp.w);
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _canvas.gameObject.SetActive(false);
        }
    }
}
