using System;
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

    private Sprite[] _frames;

    [Inject]
    private void Construct(AssetProviderService assetProviderService)
    {
        _frames = assetProviderService.Frames;
    }
        
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
                _frameImage.sprite = _card.GetFrame(_frames);
        }
        else
        {
            _card = _emptyCard.Card;
            _frameImage.color = Color.clear;
        }
    }

private void OpenCardCollection()
{
    print("press");
        
    if (Card.Rarity == RarityCard.Empty)
    {
        _deck.RememberCardLocation(transform.GetSiblingIndex(), _deckType);
    }
    else
    {
        _statisticCardWindow.gameObject.SetActive(true);
        _statisticCardWindow.Render(this);
    }
}

    public void ResetComponent()
    {
        _icon.sprite = _emptyCard.Card.UIIcon;
        _deck.RetrieveCardInCollection(this);
        _card = _emptyCard.Card;
    }
}

