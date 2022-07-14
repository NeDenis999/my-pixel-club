using System;
using System.Collections.Generic;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CardCellInDeck : CardCell
{
    [SerializeField] private EmptyCardCell _emptyCard;
    [SerializeField] private Deck _deck;
    [SerializeField] private AtackOrDefCardType _deckType;

    [SerializeField] private StatisticWindow _statisticCardWindow;

    [SerializeField] private Image _frameImage;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OpenCardCollection);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(OpenCardCollection);
    }

    public void Initialize(ICard card, StatisticWindow statisticCardWindow)
    {
        _card = (global::Card) card;
        _statisticCardWindow = statisticCardWindow;

        if (_card.Name != "Empty")
        {
            _frameImage.color = Color.white;

            if (_frameImage)
                _frameImage.sprite = _card.GetFrame();
        }
        else
        {
            _card = _emptyCard.Card;
            _frameImage.color = Color.clear;
        }
    }

private void OpenCardCollection()
{
    if (Card.Rarity == RarityCard.Empty)
    {
        _deck.RememberCardLocation(transform.GetSiblingIndex(), _deckType);
    }
    else
    {
        _deck.TurnOffCardCollection();
        _statisticCardWindow.gameObject.SetActive(true);
        _statisticCardWindow.Render(this);
    }
}

    public void ResetComponent()
    {
        _deck.RetrieveCardInCollection(this);
        Render(_emptyCard.CardData);
    }
}

