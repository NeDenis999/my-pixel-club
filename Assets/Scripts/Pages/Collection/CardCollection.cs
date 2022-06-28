using System.Collections.Generic;
using System.Linq;
using Data;
using Infrastructure.Services;
using Pages.Evolve;
using Roulette;
using UnityEngine;
using Zenject;

namespace Pages.Collection
{
    public class CardCollection : MonoBehaviour
    {
        [SerializeField] private List<CardCollectionCell> _cards;
        [SerializeField] private CardCollectionCell _cardCellTemplate;
        [SerializeField] private Transform _container;

        [SerializeField] private Shop _shop;
        [SerializeField] private RoulettePage _roulettePage;
        [SerializeField] private StartGame _startGame;
        [SerializeField] private Evolution _evolution;

        [SerializeField] private LinkBetweenCardsAndCollections _linkBetweenCardCollectionAndDeck;

        [SerializeField] private StatisticWindow _statisticWindow;

        private DataSaveLoadService _dataSaveLoadService;
        
        public List<CardCollectionCell> Cards => _cards;

        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService)
        {
            _dataSaveLoadService = dataSaveLoadService;
        }

        private void Awake()
        {
            //_cards = _dataSaveLoadService.PlayerData.InventoryDecks; //загрузить
            //_dataSaveLoadService.SetInventoryDecks(_cards); //сохранить
            
            _linkBetweenCardCollectionAndDeck.OnOpenCardCollection += () => gameObject.SetActive(true);

            _linkBetweenCardCollectionAndDeck.OnSelectedDeckCard += (cardCell, deckType, positionCardInDeck) =>
            {
                if (cardCell.Card.Rarity != RarityCard.Epmpty)
                {
                    _cards.Remove((CardCollectionCell)cardCell);
                    Destroy(cardCell.gameObject);
                }
            };

            _linkBetweenCardCollectionAndDeck.OnRetrieveCard += RetrieveCardCell;

            _shop.OnCardsBuy += AddCards;
            _startGame.OnSetStartPackCards += AddCards;
            _roulettePage.OnReceivedCard += AddCard;

            gameObject.SetActive(false);
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
            //_statisticWindow.gameObject.SetActive(false);
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

        private void Render(List<CardCollectionCell> cards)
        {
            for (int i = 0; i < _cards.Count; i++)
                cards[i].transform.SetSiblingIndex(i);
        }

        private void RetrieveCardCell(CardCell card)
        {
            var cell = Instantiate(_cardCellTemplate, _container);
            cell.SwitchComponentValue(card);
            _cards.Add(cell);
        }

        public void AddCards(Card[] newCards)
        {
            for (int i = 0; i < newCards.Length; i++)
            {
                AddCard(newCards[i]);
            }
        }

        public void AddCard(Card newCards)
        {
            if (newCards == null) throw new System.ArgumentNullException();

            var cell = Instantiate(_cardCellTemplate, _container);
            cell.Render(newCards);
            _cards.Add(cell);

            Render(_cards);
        }

        public void DeleteCards(CardCollectionCell[] cardsForDelete)
        {
            foreach (var card in cardsForDelete)
            {
                Destroy(card.gameObject);

                _cards.Remove(card);
            }
        }
    }
}
