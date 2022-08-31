using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
public class ItemObject : MonoBehaviour
{
    
    [SerializeField] private Collider _collider;
    [SerializeField] private InventoryItemData _itemData;

    private InventorySystemManager _inventorySystemManager;


    [Inject]
    public void Construct(InventorySystemManager inventorySystemManager)
    {
        _inventorySystemManager = inventorySystemManager;
    }
    

    public void Awake()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _inventorySystemManager.AddItem(_itemData);
            Destroy(gameObject);
        }
    }
}
