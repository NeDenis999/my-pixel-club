using UnityEngine;
using UnityEngine.UI;

namespace Cards.Deck.CardCell
{
    public class CardCellInDeck : global::CardCell
    {
        [SerializeField] private LinkBetweenCardsAndCollections _linkBetweenCardCollectionAndDeck;
        [SerializeField] private EmptyCardCell _emptyCard;
        [SerializeField] private AtackOrDefCardType _deckType;

        [SerializeField] private StatisticWindow _statisticCardWindow;

        private void Start()
        {
            var button = GetComponent<Button>();
            if(button != null)
                button.onClick.AddListener(OpenCardCollection);
        }

        private void OpenCardCollection()
        {
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
