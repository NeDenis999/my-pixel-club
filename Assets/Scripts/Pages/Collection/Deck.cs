using System.Collections.Generic;
using Cards.Deck.CardCell;
using Data;
using Pages.Collection;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public abstract class Deck : MonoBehaviour
{
    public event UnityAction<List<CardCellInDeck>> OnCardChanged;

    [SerializeField] protected List<CardCellInDeck> _cardsInDeck;

    [SerializeField] private CardCollection _cardCollection;
    [SerializeField] private LinkBetweenCardsAndCollections _linkBetweenCardsAndCollections;

    protected AtackOrDefCardType _deckType;
    protected DataSaveLoadService _data;
    
    public List<CardCellInDeck> CardsInDeck => _cardsInDeck;

    [Inject]
    public void Construct(DataSaveLoadService data)
    {
        _data = data;
        InitCards(data);
    }    

    private void OnEnable()
    {
        OnCardChanged?.Invoke(_cardsInDeck);

        _linkBetweenCardsAndCollections.OnSelectedDeckCard += SetCardInDeck;
    }

    private void SetCardInDeck(CardCell card, AtackOrDefCardType deckType, int positionCardInDeck)
    {
        if (deckType == _deckType)
        {
            _cardsInDeck[positionCardInDeck].SwitchComponentValue(card);
            _cardCollection.gameObject.SetActive(false);
            OnCardChanged?.Invoke(_cardsInDeck);
            SaveDesks();
        }
    }

    protected abstract void SaveDesks();
    protected abstract void InitCards(DataSaveLoadService data);
}