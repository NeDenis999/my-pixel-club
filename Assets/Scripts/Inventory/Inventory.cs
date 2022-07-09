using Infrastructure.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Inventory : MonoBehaviour
{
    private DataSaveLoadService _dataSaveLoadService;

    public List<ShopItemBottle> BottleCollection => _dataSaveLoadService.PlayerData.Items;

    [Inject]
    private void Construct(DataSaveLoadService dataSaveLoadService)
    {
        _dataSaveLoadService = dataSaveLoadService;
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
            _dataSaveLoadService.UpdateItemsData();
        }
    }

    public void AddItem(ShopItemBottle bottle)
    {
        BottleCollection.Add(bottle);
        _dataSaveLoadService.UpdateItemsData();
    }


    public void UseEnergyBottle(InventoryCell item)
    {
        _dataSaveLoadService.IncreaseEnergy(25 - _dataSaveLoadService.PlayerData.Energy);
        DestroyItem(item);
    }
}
