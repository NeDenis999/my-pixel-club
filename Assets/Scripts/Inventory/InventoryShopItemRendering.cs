using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryShopItemRendering : InventoryCategoryRendering, IPointerClickHandler
{
    [SerializeField] private Shop _shop;    

    private void Start()
    {
        _shop.OnBottleBuy += AddItem;
    }

    private void OnEnable()
    {
        Render(_inventoryItems);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Render(_inventoryItems);
    }

    private void Render(List<ShopItemBottle> items)
    {
        foreach (Transform childs in _container)
            Destroy(childs.gameObject);

        var inventoryCellList = new List<InventoryCell>();

        System.Func<ShopItem, bool> itemContainsList = (a) =>
        {
            foreach(var item in inventoryCellList)
            {
                if (item.Item.name == a.name)
                {
                    item.AmountThisItem++;
                    return true;
                }
            };

            return false;
        };

        items.ForEach(item =>
        {
            if (itemContainsList(item) == false)
            {
                var cell = Instantiate(_inventoryItemCellTemplate, _container);
                cell.Render(item);
                inventoryCellList.Add(cell);
                cell.OnUsedEnergyBottle += DestroyItem;
            }
        });
    }

    private void AddItem(ShopItemBottle item)
    {
        _inventoryItems.Add(item);
    }

    private void DestroyItem(InventoryCell item, ShopItemBottle energyBottle)
    {
        item.OnUsedEnergyBottle -= DestroyItem;
        _inventoryItems.Remove(energyBottle);
        Destroy(item.gameObject);
    }
}
