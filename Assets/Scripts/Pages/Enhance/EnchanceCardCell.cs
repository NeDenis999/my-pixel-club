using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Pages.Enhance
{
    public class EnchanceCardCell : CardCell
    {
        [SerializeField] 
        private ChangingCursorHover _changingCursorHover;

        private EnchanceCardCollection _enchanceCardCollection;
        private CardCollectionCell _cardInCollection;
        private SelectPanel _selectPanel;

        private void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(SelectCard);
        }

        private void OnDisable()
        {
            GetComponent<Button>().onClick.RemoveListener(SelectCard);
            _selectPanel.Reset();
        }

        public void Init(AssetProviderService assetProviderService, EnchanceCardCollection enchanceCardCollection, SelectPanel selectPanel)
        {
            _enchanceCardCollection = enchanceCardCollection;
            _selectPanel = selectPanel;
        }
        
        public void SetLinkOnCardInCollection(CardCollectionCell cardInCollection)
        {
            if (cardInCollection == null) throw new System.NullReferenceException();

            _cardInCollection = cardInCollection;
        }

        private void SelectCard()
        {
            _enchanceCardCollection.SelectCard(_cardInCollection);
            _selectPanel.SetPanelAboveSelectCard(this);
        }
    }
}
