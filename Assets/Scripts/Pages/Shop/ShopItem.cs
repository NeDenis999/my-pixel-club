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
    [SerializeField] private string _discription;

    [SerializeField] 
    private int _count = 1;
    
    [SerializeField] 
    private RarityCard _rarityCard;
    
    public Sprite UIIcon => _image;
    public ShopItemType TypeItem => _itemType;    
    public int Price => _price;
    public int Count => _count;
    public string Statistic => _discription;
    public ShopItem Item => this;

    public Color NameColor()
    {
        switch (_rarityCard)
        {
            case RarityCard.Empty:
                return Color.gray;
            case RarityCard.Standart:
                return Color.yellow;
            case RarityCard.Rarity:
                return Color.cyan;
        }

        return Color.red;
    }
}


