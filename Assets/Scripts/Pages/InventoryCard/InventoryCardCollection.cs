using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Roulette;
using UnityEngine;

namespace Pages.InventoryCard
{
    public class InventoryCardCollection : MonoBehaviour
    {
        [SerializeField] private List<CardCollectionInventoryCell> _cards;
        [SerializeField] private CardCollectionInventoryCell _cardCellTemplate;
        [SerializeField] private Transform _container;

        [SerializeField] private Shop _shop;
        [SerializeField] private RoulettePage roulettePage;

        [SerializeField] private StartGame _startGame;
        [SerializeField] private LinkBetweenCardsAndCollections _linkBetweenCardCollectionAndDeck;

        [SerializeField] private StatisticWindow _statisticWindow;

        public List<CardCollectionInventoryCell> Cards => _cards;

        private void Awake()
        {
            _linkBetweenCardCollectionAndDeck.OnOpenCardCollection += () => gameObject.SetActive(true);

            /*_linkBetweenCardCollectionAndDeck.OnSelectedDeckCard += (CardCell cardCell, AtackOrDefCardType deckType, int positionCardInDeck) =>
            {
                if (cardCell.Card.Rarity != RarityCard.Epmpty)
                {
                    _cards.Remove((CardCollectionInventoryCell)cardCell);
                    Destroy(cardCell.gameObject);
                }
            };*/

            _linkBetweenCardCollectionAndDeck.OnRetrieveCard += RetrieveCardCell;

            _shop.OnCardBuy += AddCard;
            roulettePage.OnReceivedCard += AddCard;

            _startGame.OnSetStartPackCard += AddCard;
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
            Render(_cards);
        }

        public void AttackSort()
        {
            _cards = _cards.OrderByDescending(e => e.Attack).ToList();
            Render(_cards);
        }

        public void DefSort()
        {
            _cards = _cards.OrderByDescending(e => e.Def).ToList();
            Render(_cards);

        }

        public void GodsSort()
        {
            RaceSort(RaceCard.Gods);
        }

        public void HumansSort()
        {
            RaceSort(RaceCard.Humans);

        }

        public void DemonsSort()
        {
            RaceSort(RaceCard.Demons);

        }

        private void RaceSort(RaceCard race)
        {
            foreach (var cardCell in _cards)
            {
                cardCell.gameObject.SetActive(false);
                if (cardCell.Card.Race == race)
                    cardCell.gameObject.SetActive(true);
            }
        }

        private void Render(List<CardCollectionInventoryCell> cards)
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                cards[i].transform.SetSiblingIndex(i);
            }
        }

        private void RetrieveCardCell(CardCell card)
        {
            var cell = Instantiate(_cardCellTemplate, _container);
            cell.SwitchComponentValue(card);
            _cards.Add(cell);
            cell.Initialization(_statisticWindow, card.Card);
        }

        private void AddCard(Card[] newCards)
        {
            for (int i = 0; i < newCards.Length; i++)
            {
                var cell = Instantiate(_cardCellTemplate, _container);
                cell.Render(newCards[i]);
                _cards.Add(cell);
                cell.Initialization(_statisticWindow, cell.Card);
            }

            Render(_cards);
        }
    }
}