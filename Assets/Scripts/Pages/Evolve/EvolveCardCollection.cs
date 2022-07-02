using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EvolveCardCollection : MonoBehaviour
{
    [SerializeField] private Evolution _evolution;

    private List<CardCollectionCell> _listCardsInCollection = new();

    [SerializeField] private EvolutionCardCell _cardCellTemplate;
    [SerializeField] private Transform _container;

    [SerializeField] private Button _doneButton;

    private CardCollectionCell _exampleCard;
    private CardCollectionCell _selectedCard;

    [HideInInspector] public EvolutionCard OneOfCardInEvolutioin;

    private void OnEnable()
    {
        _doneButton.onClick.AddListener(DoneChange);
        _selectedCard = null;
        RenderCard();
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

    private void RenderCard()
    {
        foreach (Transform card in _container)
        {
            Destroy(card.gameObject);
        }

        _exampleCard = _evolution.FirstCard.CardCell == null ? _evolution.SecondeCard.CardCell : _evolution.FirstCard.CardCell;

        for (int i = 0; i < _listCardsInCollection.Count; i++)
        {
            if (CheckCardSimilarityWhithExample(_listCardsInCollection[i].Card) && _listCardsInCollection[i].Card.Evolution == 1)
            {
                var cell = Instantiate(_cardCellTemplate, _container);
                cell.Render(_listCardsInCollection[i].Card);
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

    public void SelectCard(CardCollectionCell selectCard)
    {
        if (selectCard == null) throw new System.ArgumentNullException();

        _selectedCard = selectCard;
    }
}