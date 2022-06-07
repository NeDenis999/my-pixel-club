using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LinkBetweenCardsAndCollections : MonoBehaviour
{
    public event UnityAction<CardCell, AtackOrDefCardType, int> OnSelectedDeckCard;
    public event UnityAction<CardCell> OnRetrieveCard;
    public event UnityAction OnOpenCardCollection;

    private int _cardPositionInDeck;
    private AtackOrDefCardType _deckType;

    public void SelectCard(CardCollectionCell selectedCard)
    {
        OnSelectedDeckCard?.Invoke(selectedCard, _deckType, _cardPositionInDeck);
    }

    public void OpenCardCollection(int cardPositionInDeck, AtackOrDefCardType deckType)
    {
        _deckType = deckType;
        _cardPositionInDeck = cardPositionInDeck;
        OnOpenCardCollection?.Invoke();
    }

    public void RetrieveCard(CardCell card, CardCell emptyCard, int cardPositionInDeck, AtackOrDefCardType deckType)
    {
        OnRetrieveCard?.Invoke(card);
        OnSelectedDeckCard?.Invoke(emptyCard, deckType, cardPositionInDeck);
    }
}
