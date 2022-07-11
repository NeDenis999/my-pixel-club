using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Lumin;
using UnityEngine.UI;

public class ShopItemCell : MonoBehaviour
{
    [SerializeField] 
    private Image _icon;

    [SerializeField]
    private TextMeshProUGUI _descriptionText;
    
    [SerializeField]
    private TextMeshProUGUI _countText;
    
    [SerializeField]
    private TextMeshProUGUI _priceText;
    
    [SerializeField] 
    private ChangingCursorHover _changingCursorHover;
    
    private ConfirmWindow _confirmWindow;

    private ShopItem _shopItem;
    private int _price;

    public int Price => _price;

    public void Init(ConfirmWindow confirmWindow, AssetProviderService assetProviderService)
    {
        _confirmWindow = confirmWindow;
        _changingCursorHover.Init(assetProviderService);
    }
    
    public void Render(IShopItem item)
    {
        _icon.sprite = item.UIIcon;
        _price = item.Price;
        _shopItem = item.Item;
        
        _descriptionText.text = item.Item.name;
        _descriptionText.color = item.Item.NameColor();

        _countText.text = $"x{item.Item.Count} {item.Item.name}";
        _countText.color = item.Item.NameColor();
        
        _priceText.text = item.Item.Price + " MPC";
    }

    public void OpenConfirmWindow()
    {
        _confirmWindow.Render(_shopItem);
        _confirmWindow.gameObject.SetActive(true);
    }
}