using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketplaceButton : MonoBehaviour
{
    [SerializeField] private HideAndSeekPages _hideAndSeekPages;
    [SerializeField] private GameObject _pageToOpen;

    [SerializeField] private ShopCategoryRendering _shopCategoryRendering;

    [SerializeField] private Image _image;

    private void OnMouseDown()
    {
        _hideAndSeekPages.TurnOffAllPages();
        _pageToOpen.SetActive(true);

        if (_shopCategoryRendering != null)
            _shopCategoryRendering.SelectCategore();
    }

    private void OnMouseOver()
    {
        _image.color = Color.HSVToRGB(0, 0, 1);
    }

    private void OnMouseExit()
    {
        _image.color = Color.HSVToRGB(0, 0, 0.56f);
    }
}
