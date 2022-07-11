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
    public void AddItem(ShopItemBottle bottle)
    {
        BottleCollection.Add(bottle);
        _dataSaveLoadService.UpdateItemsData();
    }

    public void UseEnergyBottle(InventoryCell item)
    {
        _dataSaveLoadService.IncreaseEnergy(_dataSaveLoadService.PlayerData.MaxEnergy);
        DestroyItem(item);
    }

    private void DestroyItem(InventoryCell bottel)
    {
        if (bottel.AmountThisItem > 0)
            bottel.AmountThisItem--;
        else
            Destroy(bottel.gameObject);

        BottleCollection.Remove(bottel.Item);
        _dataSaveLoadService.UpdateItemsData();
    }
}
