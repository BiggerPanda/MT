using NaughtyAttributes;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
public class ItemObject : MonoBehaviour
{
    
    [SerializeField] private Collider _collider;
    [Expandable] [SerializeField] private InventoryItemData _itemData;
    [SerializeField] private Canvas _canvas;
    
    private InventorySystemManager _inventorySystemManager;


    [Inject]
    public void Construct(InventorySystemManager inventorySystemManager)
    {
        _inventorySystemManager = inventorySystemManager;
    }
    

    public void PickUp()
    {
        if(_inventorySystemManager != null)
        {
            _inventorySystemManager.AddItem(_itemData);
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _canvas.gameObject.SetActive(true);
        }
    }
     void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Quaternion temp = Quaternion.LookRotation(transform.position - other.transform.position);
            _canvas.transform.rotation = new Quaternion(0, -temp.y, 0, -temp.w);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _canvas.gameObject.SetActive(false);
        }
    }
}
