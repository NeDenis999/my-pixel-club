using Infrastructure.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Inventory : MonoBehaviour
{
    private DataSaveLoadService _data;

    public List<ShopItemBottle> BottleCollection = new();

    [Inject]
    private void Construct(DataSaveLoadService data)
    {
        _data = data;
    }
    private void DestroyItem(InventoryCell item)
    {
        if (item.AmountThisItem > 0)
        {
            item.AmountThisItem--;
        }
        else
        {
            BottleCollection.Remove(item.Item);
            Destroy(item.gameObject);
        }
    }

    public void AddItem(ShopItemBottle bottle)
    {
        BottleCollection.Add(bottle);
    }


    public void UseEnergyBottle(InventoryCell item)
    {
        _data.IncreaseEnergy(25 - _data.PlayerData.Energy);
        DestroyItem(item);
    }
}
