using System.Collections.Generic;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FarmPage.Evolve
{
    public class EvolveCardCollection : MonoBehaviour
    {
        [SerializeField] private Evolution _evolution;
        [SerializeField] private EvolutionCardCell _cardCellTemplate;
        [SerializeField] private Transform _container;
        [SerializeField] private Button _doneButton;
        [SerializeField] private SelectPanel _selectPanel;
    
        private List<CardCollectionCell> _listCardsInCollection = new();
        private CardCollectionCell _exampleCard;
        private CardCollectionCell _selectedCard;
        private AssetProviderService _assetProviderService;

        [HideInInspector] public EvolutionCard OneOfCardInEvolutioin;

        [Inject]
        private void Construct(AssetProviderService assetProviderService)
        {
            _assetProviderService = assetProviderService;
        }
    
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

        public void SetCardCollection(List<CardCollectionCell> cardCollectionCells)
        {
            if (cardCollectionCells == null) throw new System.InvalidOperationException();

            _listCardsInCollection.Clear();
            _listCardsInCollection.AddRange(cardCollectionCells);
        }

        public void SelectCard(CardCollectionCell selectCard)
        {
            if (selectCard == null) 
                throw new System.ArgumentNullException();

            _selectedCard = selectCard;
        }

        private void RenderCards()
        {
            foreach (Transform card in _container) 
                Destroy(card.gameObject);

            _exampleCard = _evolution.FirstCard.CardCell == null ? _evolution.SecondeCard.CardCell : _evolution.FirstCard.CardCell;
        
            for (int i = 0; i < _listCardsInCollection.Count; i++)
            {
                if (CheckCardSimilarityWhithExample(_listCardsInCollection[i].Card) && _listCardsInCollection[i].Evolution == 1)
                {
                    var cell = Instantiate(_cardCellTemplate, _container);
                    cell.Init(this, _selectPanel, _assetProviderService);
                    cell.Render(_listCardsInCollection[i].CardData);
                    cell.SetLinkOnCardInCollection(_listCardsInCollection[i]);
                }
            }
        }

        private bool CheckCardSimilarityWhithExample(Card card)
        {
            if (_exampleCard == null) return true;

            if (card.UIIcon.name == _exampleCard.UIIcon.name)
                return true;

            return false;
        }

        private void DoneChange()
        {
            if (OneOfCardInEvolutioin == null) throw new System.InvalidOperationException();

            if (OneOfCardInEvolutioin.CardCell != null)
                _listCardsInCollection.Add(OneOfCardInEvolutioin.CardCell);

            if (_selectedCard != null)
            {
                _listCardsInCollection.Remove(_selectedCard);
                OneOfCardInEvolutioin.SetCard(_selectedCard);
                _selectedCard = null;
            }
        }
    }
}