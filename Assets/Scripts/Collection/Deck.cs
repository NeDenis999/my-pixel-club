using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class Deck : MonoBehaviour
{
    public event UnityAction<List<CardCellInDeck>> OnCardChanged;

    [SerializeField] private List<CardCellInDeck> _cardsInDeck;

    [SerializeField] private CardCollection _cardCollection;
    [SerializeField] private LinkBetweenCardsAndCollections _linkBetweenCardsAndCollections;

    protected AtackOrDefCardType _deckType;

    public List<CardCellInDeck> CardsInDeck => _cardsInDeck;

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