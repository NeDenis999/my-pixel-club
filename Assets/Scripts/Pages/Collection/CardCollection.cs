using System.Collections.Generic;
using System.Linq;
using Data;
using Infrastructure.Services;
using UnityEngine;
using Zenject;


public class CardCollection : CardCollectionSort
{
    [SerializeField] private CardCollectionCell _cardCellTemplate;
    [SerializeField] private Transform _container;

    [SerializeField] private StatisticWindow _statisticWindow;

    private DataSaveLoadService _dataSaveLoadService;
        
    public List<CardCollectionCell> Cards => _cards;

    [Inject]
    private void Construct(DataSaveLoadService dataSaveLoadService)
    {
        _dataSaveLoadService = dataSaveLoadService;
    }

    private void Awake()
    {
        AddCards(_dataSaveLoadService.PlayerData.InventoryDecks);
    }

    private void OnEnable()
    {
        void a()
        {
            foreach (var item in _cards)
            {
                item.gameObject.SetActive(true);
            }
        }
        a();

        _cards = _cards.OrderByDescending(e => e.Card.Rarity).ToList();
        RenderCardsSiblingIndex();
        //_statisticWindow.gameObject.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        SaveCards();
    }

    private void SaveCards()
    {
        List<Card> cardCollection = new();

        foreach (var cardCell in _cards)
        {
            cardCollection.Add(cardCell.Card);
        }

        _dataSaveLoadService.SetInventoryDecks(cardCollection.ToArray());
    }

    public void AddCards(Card[] newCards)
    {
        for (int i = 0; i < newCards.Length; i++)
        {
            AddCard(newCards[i]);
        }
    }

    public void AddCard(Card newCards)
    {
        if (newCards == null) throw new System.ArgumentNullException();

        var cell = Instantiate(_cardCellTemplate, _container);
        cell.Render(newCards);
        _cards.Add(cell);
    }

    public void DeleteCards(CardCollectionCell[] cardsForDelete)
    {
        foreach (var card in cardsForDelete)
        {
            Destroy(card.gameObject);

            _cards.Remove(card);
        }

        SaveCards();
    }
}

