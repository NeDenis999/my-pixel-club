using System.Collections.Generic;
using Pages.Enhance.Card_Statistic;
using UnityEngine;

namespace Pages.Enhance
{
    public class EnchanceCardsForDeleteCollection : CardCollectionSort
    {
        [SerializeField] private EnchanceCardForDeleteCell _cardCellTemplate;
        [SerializeField] private Transform _container;

        [SerializeField] private PossibleLevelUpSlider possibleLevelUpSlider;
        public PossibleLevelUpSlider PossibleLevelUpSlider => possibleLevelUpSlider;

        private List<CardCollectionCell> _cardsForDelete = new();
        public List<CardCollectionCell> CardForDelete => _cardsForDelete;

        private void OnEnable()
        {
            _cards.Clear();
        }

        private void OnDisable()
        {
            ClearCardForDeleteCollection();
        }

        private void ClearCardForDeleteCollection()
        {
            foreach (Transform child in _container)
                Destroy(child.gameObject);

            _cards.Clear();
            _cardsForDelete.Clear();
        }

        public void DisplayCardsForDelete(List<CardCollectionCell> cardsForDelete)
        {
            ClearCardForDeleteCollection();

            if (cardsForDelete == null) throw new System.ArgumentNullException();

            RenderCards();
        
            void RenderCards()
            {
                for (int i = 0; i < cardsForDelete.Count; i++)
                {
                    var cell = Instantiate(_cardCellTemplate, _container);
                    cell.Render(cardsForDelete[i].CardData);
                    cell.SetLinkOnCardInCollection(cardsForDelete[i]);
                    _cards.Add(cell);
                }
            }
        }

        public void AddToDeleteCollection(CardCollectionCell cardForDelete)
        {
            if (cardForDelete == null) throw new System.ArgumentNullException();

            _cardsForDelete.Add(cardForDelete);

            possibleLevelUpSlider.IncreasePossibleSliderLevelPoints(cardForDelete);
        }

        public void RetrieveCard(CardCollectionCell cardForDelete)
        {
            if (_cardsForDelete.Contains(cardForDelete) == false) throw new System.ArgumentOutOfRangeException();

            _cardsForDelete.Remove(cardForDelete);
            possibleLevelUpSlider.DecreasePossibleSliderLevelPoints(cardForDelete);
        }
    }
}
