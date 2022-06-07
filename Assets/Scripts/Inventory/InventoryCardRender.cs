using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryCardRender : InventoryCategoryRendering, IPointerClickHandler
{
    [SerializeField] private CardCollection _cardCollection;

    public void OnPointerClick(PointerEventData eventData)
    {
        Render(_cardCollection.Cards);
    }

    private void Render(List<CardCollectionCell> items)
    {
        foreach (Transform childs in _container)
            Destroy(childs.gameObject);


        items.ForEach(item =>
        {
            var cell = Instantiate(_inventoryItemCellTemplate, _container);
            cell.Render(item);
        });
    }
}
