using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    public event UnityAction<int> OnCristalBuy;
    public event UnityAction<ShopItemBottle> OnBottleBuy;
    public event UnityAction<Card[]> OnCardsBuy;

    private ShopItemCardPack _cardsPack;

    [Header("Chance per procent")]
    [SerializeField] private float _dropChance;

    public void BuyItem(ShopItem shopItem)
    {
        if (shopItem.TypeItem == ShopItemType.Cristal)
            OnCristalBuy?.Invoke(5);

        if (shopItem is ShopItemBottle)
            OnBottleBuy?.Invoke((ShopItemBottle)shopItem);
    }

    public void BuyCard(ShopItemCardPack shopItem)
    {
        _cardsPack = shopItem;
        OnCardsBuy?.Invoke(GetRandomCards((int)shopItem.TypeItem));
    }

    private Card[] GetRandomCards(int amountCard)
    {
        Card[] cards = new Card[amountCard];

        for (int i = 0; i < amountCard; i++)
        {
            if (Random.Range(0, Mathf.RoundToInt(1 / (_dropChance / 100))) == 1)
                cards[i] = GetRandomCard(_cardsPack.AllRarityCards);
            else
                cards[i] = GetRandomCard(_cardsPack.AllStandardCards);
        }

        return cards;
    }

    private Card GetRandomCard(Card[] cards)
    {
        return cards[Random.Range(0, cards.Length)];
    }
}