using TMPro;
using UnityEngine;
using System;
using UnityEngine.Events;

public class ConfirmWindow : MonoBehaviour 
{
    public event UnityAction<int> OnWithdrawMoney;

    [SerializeField] private TMP_Text _quantityMoneyToBuy, _itemType;

    [SerializeField] private Shop _shop;
    [SerializeField] private GoldWallet _goldWallet;

    private ShopItem _shopItem;

    public void Render(ShopItem item)
    {
        _shopItem = item;
        _quantityMoneyToBuy.text = "Price: " + item.Price.ToString();
        _itemType.text = item.name;
    }

    public void Buy()
    {
        OnWithdrawMoney?.Invoke(_shopItem.Price);

        if (_shopItem.Item is ShopItemCardPack)
            _shop.BuyCard((ShopItemCardPack)_shopItem);
        else        
            _shop.BuyItem(_shopItem);
    }
}
