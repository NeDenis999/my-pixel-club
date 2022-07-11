using System.Collections;
using System.Collections.Generic;
using Collection;
using UnityEngine;

public class CardInventory : CardCollectionSort<InventoryCardCell>
{
    [SerializeField] private CardCollection _cardCollection;
    [SerializeField] private AttackDeck _attackDeck;
    [SerializeField] private Transform _container;
    [SerializeField] private InventoryCardCell _cardCellTemplayte;

    [SerializeField] private InventoryCardStatistic _cardStatistic;

    private void OnEnable()
    {
        _cards.Clear();

        foreach (Transform cell in _container)
        {
            Destroy(cell.gameObject);
        }

        Render(_cardCollection.Cards);
        Render(_attackDeck.CardsInDeck);
    }

    private void Render<K>(List<K> cardCells) where K : CardCell
    {
        foreach (var card in cardCells)
        {
            if (card.Card.Rarity != RarityCard.Empty)
            {
                var cell = Instantiate(_cardCellTemplayte, _container);
                cell.Render(card.CardData, _cardStatistic);
                _cards.Add(cell);
            }
        }
    }
}
