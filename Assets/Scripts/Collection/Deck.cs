using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cards.Deck.CardCell;
using Data;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public abstract class Deck : MonoBehaviour
{
    public event UnityAction<List<CardCellInDeck>> OnCardChanged;

    [SerializeField] private List<CardCellInDeck> _cardsInDeck;

    [SerializeField] private CardCollection _cardCollection;
    [SerializeField] private LinkBetweenCardsAndCollections _linkBetweenCardsAndCollections;

    protected AtackOrDefCardType _deckType;

    public List<CardCellInDeck> CardsInDeck => _cardsInDeck;

    [Inject]
    public void Construct(PlayerDataScriptableObject data)
    {
        for (int i = 0; i < data.PlayerData.AttackDecks.Length && i < _cardsInDeck.Count; i++)
            if (data.PlayerData.AttackDecks[i] != null && _cardsInDeck[i] != null)
                _cardsInDeck[i].Render(data.PlayerData.AttackDecks[i]);
    }
    
    private void OnEnable()
    {        
        _linkBetweenCardsAndCollections.OnSelectedDeckCard += SetCardInDeck;
    }

    private void SetCardInDeck(CardCell card, AtackOrDefCardType deckType, int positionCardInDeck)
    {
        if (deckType == _deckType)
        {
            _cardsInDeck[positionCardInDeck].SwitchComponentValue(card);
            _cardCollection.gameObject.SetActive(false);
            OnCardChanged?.Invoke(_cardsInDeck);
        }
    }
}