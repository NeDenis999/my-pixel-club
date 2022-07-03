using TMPro;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfirmWindow : MonoBehaviour 
{
    public event UnityAction<int> OnWithdrawMoney;

    [SerializeField] private TMP_Text _quantityMoneyToBuy, _itemType;

    [SerializeField] 
    private TMP_Text _descriptionText;
    
    [SerializeField] 
    private TMP_Text _countText;
    
    [SerializeField] private Shop _shop;
    [SerializeField] private Image _iconImage;
    
    private ShopItem _shopItem;


    public void Render(ShopItem item)
    {
        _shopItem = item;
        _quantityMoneyToBuy.text = item.name + '\n' + "Price: " + item.Price.ToString();
        _itemType.text = item.name;
        _iconImage.sprite = item.UIIcon;
        _descriptionText.text = item.name;
        _descriptionText.color = item.NameColor();
        _countText.text = $"x{item.Count}";
    }

    public void Buy()
    {
        OnWithdrawMoney?.Invoke(_shopItem.Price);

        if (_shopItem.Item is ShopItemCardPack)
            _shop.BuyCard((ShopItemCardPack)_shopItem);
        else        
            _shop.BuyItem(_shopItem);

        gameObject.SetActive(false);
    }
}
