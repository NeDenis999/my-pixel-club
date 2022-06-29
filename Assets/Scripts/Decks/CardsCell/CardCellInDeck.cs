using System;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Cards.Deck.CardCell
{
    public class CardCellInDeck : global::CardCell
    {
        [SerializeField] private LinkBetweenCardsAndCollections _linkBetweenCardCollectionAndDeck;
        [SerializeField] private EmptyCardCell _emptyCard;
        [SerializeField] private AtackOrDefCardType _deckType;

        [SerializeField] private StatisticWindow _statisticCardWindow;

        [SerializeField] private Image _frameImage;

        [SerializeField] private Button _button;

        private Sprite[] _frames;

        [Inject]
        private void Construct(AssetProviderService assetProviderService)
        {
            _frames = assetProviderService.Frames;
        }
        
        private void Awake()
        {
            _button.onClick.AddListener(OpenCardCollection);
        }

        public void Initialize(ICard card, StatisticWindow statisticCardWindow)
        {
            _card = (global::Card) card;
            _statisticCardWindow = statisticCardWindow;

            if (_card.Name != "Empty")
            {
                _frameImage.color = Color.white;

                if (_frameImage)
                {
                    _frameImage.sprite = _card.GetFrame(_frames);
                }
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
        
        if (Card.Rarity == RarityCard.Epmpty)
        {
            _linkBetweenCardCollectionAndDeck.OpenCardCollection(transform.GetSiblingIndex(), _deckType);
        }
        else
        {
            _statisticCardWindow.gameObject.SetActive(true);
            _statisticCardWindow.Render(this);
        }
    }

        public void ResetComponent()
        {
            _linkBetweenCardCollectionAndDeck.RetrieveCard(this, _emptyCard, transform.GetSiblingIndex(), _deckType);
        }
    }
}
