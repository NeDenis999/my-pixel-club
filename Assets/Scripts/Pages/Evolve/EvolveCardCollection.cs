using Pages.Collection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EvolveCardCollection : MonoBehaviour
{
    [SerializeField] private Evolution _evolution;
    [SerializeField] private CardCollection _cardCollection;

    private List<CardCollectionCell> _listCardsInCollection = new();

    [SerializeField] private EvolutionCardCell _cardCellTemplate;
    [SerializeField] private Transform _container;

    [SerializeField] private Button _doneButton;

    private CardCollectionCell _exampleCard;
    private CardCollectionCell _selectedCard;

    private int _sortCounter;

    [HideInInspector] public EvolutionCard OneOfCardInEvolutioin;

    private void OnEnable()
    {
        _listCardsInCollection.Clear();
        _listCardsInCollection.AddRange(_cardCollection.Cards);

        _doneButton.onClick.AddListener(DoneChange);
        _selectedCard = null;
        RenderCard();
    }

    private void OnDisable()
    {
        _doneButton.onClick.RemoveListener(DoneChange);
        _sortCounter = 0;
    }

    private void RenderCard()
    {
        foreach (Transform card in _container)
        {
            Destroy(card.gameObject);
        }

        _exampleCard = _evolution.FirstCard.CardCell == null ? _evolution.SecondeCard.CardCell : _evolution.FirstCard.CardCell;

        for (int i = 0; i < _cardCollection.Cards.Count; i++)
        {
            if (CheckCardSimilarityWhithExample(_listCardsInCollection[i].Card) && _listCardsInCollection[i].Card.Evoulution == 1)
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
        {
            if (_sortCounter > 0)
                return true;

            _sortCounter++;
        }

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
        _selectedCard = selectCard;
    }
}