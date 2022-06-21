using TMPro;
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

        [SerializeField] 
        private TextMeshProUGUI _attackText;

        [SerializeField] 
        private TextMeshProUGUI _defenseText;

        [SerializeField] 
        private TextMeshProUGUI _healthText;

        [SerializeField] 
        private GameObject _statsPanel;

        [SerializeField] 
        private Sprite _noneCardSprite;

        private void Start()
        {
            if (_card == null) _card = _emptyCard.Card;

            var button = GetComponent<Button>();
            if(button != null)
                button.onClick.AddListener(OpenCardCollection);

            if (_card)
                Icon.transform.localPosition = Icon.transform.localPosition.ToMove(_card.DirectionView);
            //UpdatePanelStats(_card);
        }
        
        public void Initialize(ICard card, StatisticWindow statisticCardWindow)
        {
            _card = (global::Card) card;
            _statisticCardWindow = statisticCardWindow;
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

        public override void UpdatePanelStats(ICard cardForRender)
        {
            if (!_statsPanel || cardForRender == null)
                return;
            
            if (cardForRender.Attack != 0 && cardForRender.Def != 0)
            {
                _statsPanel.SetActive(true);
                _attackText.text = Attack.ToString();
                _defenseText.text = Def.ToString();
                _healthText.text = Health.ToString();
            }
            else
            {
                _statsPanel.SetActive(false);
                _icon.sprite = _noneCardSprite;
            }
        }
    }
}
