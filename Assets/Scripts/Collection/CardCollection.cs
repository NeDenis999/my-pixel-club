using System;
using System.Collections.Generic;
using System.Linq;
using Collection;
using Data;
using Infrastructure.Services;
using UnityEngine;
using Zenject;


public class CardCollection : CardCollectionSort<CardCollectionCell>
{
    [SerializeField] private CardCollectionCell _cardCellTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private StatisticWindow _statisticWindow;

    [SerializeField] 
    private AttackDeck _attackDeck;
    
    [SerializeField] 
    private DefenceDeck _defenceDeck;

    private DataSaveLoadService _dataSaveLoadService;

    public List<CardCollectionCell> Cards => _cards;

    [Inject]
    private void Construct(DataSaveLoadService dataSaveLoadService)
    {
        _dataSaveLoadService = dataSaveLoadService;
    }

    private void Awake()
    {
        AddCards(_dataSaveLoadService.PlayerData.InventoryDecksData);
    }

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        ActiveAllCards();

        _cards = _cards.OrderByDescending(e => e.Card.Rarity).ThenByDescending(e => e.Power).ToList();
        RenderCardsSiblingIndex();
    }

    private void ActiveAllCards()
    {
        foreach (var item in _cards) 
            item.gameObject.SetActive(true);
    }
    
    private void SaveCards() => 
        _dataSaveLoadService.SetInventoryDecks(_cards);

    public void AddCards(CardData[] newCards)
    {
        foreach (var cardData in newCards)
            AddCard(cardData);
    }

    public void AddCard(CardData newCards)
    {
        var cell = Instantiate(_cardCellTemplate, _container);
        cell.InitBase(_attackDeck, _defenceDeck);
        cell.Render(newCards);
        _cards.Add(cell);

        SaveCards();
    }

    public void AddCardCell(CardCell cardCell)
    {
        if (cardCell == null) throw new System.ArgumentNullException();

        var newCell = Instantiate(_cardCellTemplate, _container);
        newCell.InitBase(_attackDeck, _defenceDeck);
        newCell.Render(cardCell.CardData);
        _cards.Add(newCell);

        SaveCards();
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

