using UnityEngine;
using Zenject;

public class InventoryInstaller : MonoInstaller
{
    public InventorySystemManager inventorySystemManager;
    public override void InstallBindings()
    {
        Container.BindInstance(inventorySystemManager).AsSingle();
    }
}