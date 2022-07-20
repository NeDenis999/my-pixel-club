using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InventoryShopItemRendering : InventoryCategoryRendering
{
    [SerializeField] private Inventory _inventory;

    private void OnEnable()
    {
        Render();
        GetComponent<Button>().onClick.AddListener(Render);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(Render);
    }

    private void Render()
    {
        foreach (Transform childs in _container)
            Destroy(childs.gameObject);

        var inventoryCellList = new List<InventoryCell>();

        System.Func<ShopItem, bool> itemContainsList = (a) =>
        {
            foreach (var item in inventoryCellList)
            {
                if (item.Bottel.name == a.name)
                {
                    item.AmountThisItem++;
                    return true;
                }
            };

            return false;
        };

        _inventory.BottleCollection.ForEach(item =>
        {
            if (itemContainsList(item) == false)
            {
                var cell = Instantiate(_inventoryItemCellTemplate, _container);
                cell.Render(item, _inventory);
                inventoryCellList.Add(cell);
            }
        });
    }
}   
