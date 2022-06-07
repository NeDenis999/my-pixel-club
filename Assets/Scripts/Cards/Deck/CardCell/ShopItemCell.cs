using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItemCell : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private ConfirmWindow _confirmWindow;

    private ShopItem _shopItem;
    private int _price;

    public int Price => _price;   

    public void Render(IShopItem item)
    {
        _icon.sprite = item.UIIcon;
        _price = item.Price;
        _shopItem = item.Item;
    }

    public void OpenConfirmWindow()
    {
        _confirmWindow.Render(_shopItem);
        _confirmWindow.gameObject.SetActive(true);
    }
}