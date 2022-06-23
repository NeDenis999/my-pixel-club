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

        [SerializeField] 
        private bool _isStartMoveDirection;

        [SerializeField] 
        private Image _frameImage;

        [SerializeField] 
        private Sprite[] _frames;
        
        private void Start()
        {
            if (_card == null) _card = _emptyCard.Card;

            var button = GetComponent<Button>();
            if(button != null)
                button.onClick.AddListener(OpenCardCollection);

            if (_card)
            {
                if (_isStartMoveDirection)
                    Icon.transform.localPosition = Icon.transform.localPosition.ToMove(_card.DirectionView);
                
                if (_frameImage && _frames.Length != 0)
                {
                    switch (_card.Race)
                    {
                        case RaceCard.Demons:
                            _frameImage.sprite = _frames[0];
                            break;
                        
                        case RaceCard.Gods:
                            _frameImage.sprite = _frames[1];
                            break;
                        
                        case RaceCard.Humans:
                            _frameImage.sprite = _frames[2];
                            break;
                    }
                }
            }

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
