using System;
using System.Collections.Generic;
using Data;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public abstract class Deck : MonoBehaviour
{
    public event Action<List<CardCellInDeck>> OnCardChanged;

    [SerializeField] protected List<CardCellInDeck> _cardsInDeck;

    [SerializeField] private CardCollection _cardCollection;

    private int _cardPositionInDeck;
    private AtackOrDefCardType _deckType;
    public AtackOrDefCardType WritenDeck => _deckType;

    protected DataSaveLoadService _data;
    
    public List<CardCellInDeck> CardsInDeck => _cardsInDeck;

    protected abstract void SaveDecks();
    protected abstract void InitCards(DataSaveLoadService data);

    [Inject]
    public void Construct(DataSaveLoadService data)
    {
        _data = data;
        InitCards(data);
    }    

    private void OnEnable()
    {
        OnCardChanged?.Invoke(_cardsInDeck);
    }

    private void OnDisable()
    {
        SaveDecks(); 
    }

    private void OnApplicationQuit()
    {
        SaveDecks();
    }

    public void RememberCardLocation(int cardPositionInDeck, AtackOrDefCardType fromDeck)
    {
        _cardCollection.gameObject.SetActive(true);
        _cardPositionInDeck = cardPositionInDeck;
        _deckType = fromDeck;
    }

    public void SetCardInDeck(CardCollectionCell card)
    {
        if (card == null) throw new System.ArgumentNullException(); 

        _cardsInDeck[_cardPositionInDeck].Render(card.CardData);
        _cardCollection.DeleteCards(new[] { card });
        OnCardChanged?.Invoke(_cardsInDeck);
        SaveDecks();
        _cardCollection.gameObject.SetActive(false);
    }

    public void RetrieveCardInCollection(CardCell card)
    {
        _cardCollection.AddCardCell(card);
    }
}