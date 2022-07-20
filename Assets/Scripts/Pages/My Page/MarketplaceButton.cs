using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketplaceButton : MonoBehaviour
{
    [SerializeField] private HideAndSeekPages _hideAndSeekPages;
    [SerializeField] private Page _pageToOpen;

    [SerializeField] private Button _eventRenderingButton;

    [SerializeField] private Image _image;

    private void OnEnable()
    {
        _image.color = Color.HSVToRGB(0, 0, 0.56f);
    }

    private void OnMouseDown()
    {
        _hideAndSeekPages.TurnOffAllPages();
        _pageToOpen.StartShowSmooth();

        if (_eventRenderingButton != null)
            _eventRenderingButton.onClick?.Invoke();
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
