using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Cristal", menuName = "ScriptableObjects/Shop/Cristal")]
public class ShopItemCristal : ShopItem, IShopItem, IPrize
{
    public void TakeItem(IIncreaserWalletValueAndCardsCount increaser, int amountValue)
    {
        increaser.AccrueCristal(amountValue);
    }
}
