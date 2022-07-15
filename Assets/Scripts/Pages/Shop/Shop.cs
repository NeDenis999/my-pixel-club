using Data;
using Pages.Shop;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    private ShopItemCardPack _cardsPack;

    [Header("Chance per procent")]
    [SerializeField] private float _dropChance;

    [SerializeField] 
    private ShopCategoryRendering _startCategory;

    [SerializeField] private CardCollection _cardCollection;

    [SerializeField] private Inventory _inventory;
        
    [SerializeField] 
    private PurchaseWindow _purchaseWindow;

    [SerializeField] private CristalWallet _cristalWallet;
    
    private void Start()
    {
        _startCategory.SelectCategore();
    }
    
    public void BuyItem(ShopItem shopItem)
    {
        if (shopItem.TypeItem == ShopItemType.Cristal)
            _cristalWallet.AddÑurrency(5);

        if (shopItem is ShopItemBottle)
            _inventory.AddItem((ShopItemBottle)shopItem);
    }

    public void BuyCard(ShopItemCardPack shopItem)
    {
        _cardsPack = shopItem;
        Card[] randomCardsData = GetRandomCards((int) shopItem.TypeItem);
        _cardCollection.AddCards(randomCardsData.ToCardsData());
        if(randomCardsData.Length > 0)
            _purchaseWindow.StartOpen(shopItem, randomCardsData);
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