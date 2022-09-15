using UnityEngine;
using Zenject;
using MT.Inventory;

public class InventoryInstaller : MonoInstaller
{
    public InventorySystemManager inventorySystemManager;
    public InventoryController inventoryController;
    public override void InstallBindings()
    {
        Container.BindInstance(inventorySystemManager).AsSingle();
        Container.BindInstance(inventoryController).AsSingle();
    }
}