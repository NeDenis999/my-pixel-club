using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketplaceButton : MonoBehaviour
{
    [SerializeField] private HideAndSeekPages _hideAndSeekPages;
    [SerializeField] private GameObject _pageToOpen;

    [SerializeField] private ShopCategoryRendering _shopCategoryRendering;

    private void OnMouseDown()
    {
        _hideAndSeekPages.TurnOffAllPages();
        _pageToOpen.SetActive(true);

        if (_shopCategoryRendering != null)
            _shopCategoryRendering.SelectCategore();
    }
}
