using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCategoryRendering : MonoBehaviour
{
    [SerializeField] private List<ShopItem> _shopItems;
    [SerializeField] private ShopItemCell _shopItemCellTemplate;
    [SerializeField] private Transform _container;

    public void SelectCategore()
    {
        Render();
    }

    private void Render()
    {
        foreach (Transform childs in _container)
            Destroy(childs.gameObject);


        _shopItems.ForEach(item =>
        {
            var cell = Instantiate(_shopItemCellTemplate, _container);
            cell.Render(item);
        });
    }
}
