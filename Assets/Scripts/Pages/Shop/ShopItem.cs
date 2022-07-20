using System;
using UnityEngine;

public enum ShopItemType
{
    MinPackCard = 1,
    MidPackCard = 5,
    MaxPackCard = 10,
    EnergyBottle,
    HealthBottle,
    Cristal
}

public abstract class ShopItem : ScriptableObject, IShopItem
{
    [SerializeField] private Sprite _image;
    [SerializeField] private ShopItemType _itemType;
    [SerializeField] private int _price;

    [SerializeField] 
    private int _count;
    
    public Sprite UIIcon => _image;
    public ShopItemType TypeItem => _itemType;    
    public int Price => _price;
    public int Count => _count;
    public ShopItem Item => this;
}


