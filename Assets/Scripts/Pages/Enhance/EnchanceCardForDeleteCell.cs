using FarmPage.Enhance.Card_Statistic;
using UnityEngine;
using UnityEngine.UI;

namespace FarmPage.Enhance
{
    [RequireComponent(typeof(Button))]
    public class EnchanceCardForDeleteCell : CardCollectionCell
    {
        [SerializeField] 
        private GameObject _selectPanel;

        [SerializeField]
        private Button _selectButton;
        
        [SerializeField]
        private Button _cardButton;
        
        private EnchanceCardsForDeleteCollection _enchanceCardForDeleteCollection;
        private CardCollectionCell _cardInCollection;
        private Enchance _enchance;
        private EnhanceCardForDeleteStatistic _cardStatistic;

        protected override void Awake()
        {
            
        }

        private void OnEnable()
        {
            _cardButton.onClick.AddListener(SelectCard);
            _selectButton.onClick.AddListener(UnselectCard);
            _selectPanel.SetActive(false);
        }

        private void OnDisable()
        {
            _cardButton.onClick.RemoveListener(SelectCard);
            _selectButton.onClick.RemoveListener(UnselectCard);
        }

        public void Init(EnchanceCardsForDeleteCollection enchanceCardsForDeleteCollection, Enchance enchance, EnhanceCardForDeleteStatistic enhanceCardForDeleteStatistic)
        {
            _enchanceCardForDeleteCollection = enchanceCardsForDeleteCollection;
            _enchance = enchance;
            _cardStatistic = enhanceCardForDeleteStatistic;
        }
        
        public void SetLinkOnCardInCollection(CardCollectionCell cardInCollection)
        {
            if (cardInCollection == null) 
                throw new System.NullReferenceException();

            _cardInCollection = cardInCollection;
        }

        private void SelectCard()
        {
            if (_enchance.UpgradeCard.CardCell.Level + _enchanceCardForDeleteCollection.PossibleLevelUpSlider.HowMuchIncreaseLevel < 25)
            {
                _enchanceCardForDeleteCollection.AddToDeleteCollection(_cardInCollection);
                _selectPanel.SetActive(true);
                _cardStatistic.Render(_cardInCollection);
            }
        }

        private void UnselectCard()
        {
            _enchanceCardForDeleteCollection.RetrieveCard(_cardInCollection);
            _selectPanel.SetActive(false);
        }
    }
}
