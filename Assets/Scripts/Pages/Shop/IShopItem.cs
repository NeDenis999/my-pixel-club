using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopItem 
{
    public Sprite UIIcon { get; }
    public int Price { get; }
    public ShopItem Item { get; }
}
