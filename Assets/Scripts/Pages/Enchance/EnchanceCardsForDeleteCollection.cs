using Infrastructure.Services;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class EnchanceCardsForDeleteCollection : CardCollectionSort
{
    [SerializeField] private EnchanceCardForDeleteCell _cardCellTemplate;
    [SerializeField] private Transform _container;

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
    }

    public void DisplayCardsForDelete(List<CardCollectionCell> cardsForDelete)
    {
        ClearCardForDeleteCollection();

        if (cardsForDelete == null) throw new System.ArgumentNullException();

        _cards.AddRange(cardsForDelete);

        RenderCards();
        
        void RenderCards()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            var cell = Instantiate(_cardCellTemplate, _container);
            cell.Render(_cards[i].Card);
            cell.SetLinkOnCardInCollection(_cards[i]);
        }
    }
    }

    public void AddToDeleteCollection(CardCollectionCell cardForDelete)
    {
        if (cardForDelete == null) throw new System.ArgumentNullException();

        _cardsForDelete.Add(cardForDelete);
    }

    public void RetrieveCard(CardCollectionCell card)
    {
        if (_cardsForDelete.Contains(card) == false) throw new System.ArgumentOutOfRangeException();

        _cardsForDelete.Remove(card);
    }
}
