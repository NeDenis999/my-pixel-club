using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Card Pack", menuName = "ScriptableObjects/Shop/Card Pack")]
public class ShopItemCardPack : ShopItem, IShopItem
{

    [SerializeField] private Card[] _allStandardCards;
    [SerializeField] private Card[] _allRarityCards;

    public Card[] AllStandardCards => _allStandardCards;
    public Card[] AllRarityCards => _allRarityCards;
}
