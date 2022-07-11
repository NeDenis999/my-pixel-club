using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pages.Enhance
{
    public class EnchanceCardCollection : MonoBehaviour
    {
        [SerializeField] private Enchance _ennchance;

        [SerializeField] private EnchanceCardsForDeleteCollection _enchanceCardsForDeleteCollection;

        private List<CardCollectionCell> _listCardsInCollection = new();

        [SerializeField] private EnchanceCardCell _cardCellTemplate;
        [SerializeField] private Transform _container;

        [SerializeField] private Button _doneButton;

        private CardCollectionCell _selectedCard;

        [HideInInspector] public EnchanceUpgradeCard UpgradeCard;

        private void OnEnable()
        {
            _doneButton.onClick.AddListener(DoneChange);
            _selectedCard = null;
            RenderCards();
        }

        private void OnDisable()
        {
            _doneButton.onClick.RemoveListener(DoneChange);
        }

        public void InitCardCollection(List<CardCollectionCell> cardCollectionCells)
        {
            if (cardCollectionCells == null) throw new System.InvalidOperationException();

            _listCardsInCollection.Clear();
            _listCardsInCollection.AddRange(cardCollectionCells);
        }

        public void SelectCard(CardCollectionCell selectCard)
        {
            if (selectCard == null) throw new System.ArgumentNullException();

            _selectedCard = selectCard;
        }

        private void RenderCards()
        {
            foreach (Transform card in _container)
            {
                Destroy(card.gameObject);
            }

            for (int i = 0; i < _listCardsInCollection.Count; i++)
            {
                EnchanceCardCell cell = Instantiate(_cardCellTemplate, _container);
                cell.Render(_listCardsInCollection[i].CardData);
                cell.SetLinkOnCardInCollection(_listCardsInCollection[i]);
            }
        }

        private void DoneChange()
        {
            if (UpgradeCard == null) throw new System.InvalidOperationException();

            if (UpgradeCard.CardCell != null && _selectedCard != null)
                _listCardsInCollection.Add(UpgradeCard.CardCell);

            if (_selectedCard != null)
            {
                _listCardsInCollection.Remove(_selectedCard);
                UpgradeCard.SetCardForUpgrade(_selectedCard);
                _selectedCard = null;

                _enchanceCardsForDeleteCollection.DisplayCardsForDelete(_listCardsInCollection);
            }
        }
    }
}
