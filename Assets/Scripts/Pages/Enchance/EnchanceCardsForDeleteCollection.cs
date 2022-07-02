using Infrastructure.Services;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EnchanceCardsForDeleteCollection : CardCollectionSort
{
    [SerializeField] private EnchanceCardForDeleteCell _cardCellTemplate;
    [SerializeField] private Transform _container;

    [SerializeField] private PosibleLevelUpSlider _posibleLevelUpSlider;
    public PosibleLevelUpSlider PosibleLevelUpSlider => _posibleLevelUpSlider;

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

        cardsForDelete.AddRange(cardsForDelete);

        RenderCards();
        
        void RenderCards()
        {
            for (int i = 0; i < cardsForDelete.Count; i++)
            {
                var cell = Instantiate(_cardCellTemplate, _container);
                cell.Render(cardsForDelete[i].Card);
                cell.SetLinkOnCardInCollection(cardsForDelete[i]);
                _cards.Add(cell);
            }
        }
    }

    public void AddToDeleteCollection(CardCollectionCell cardForDelete)
    {
        if (cardForDelete == null) throw new System.ArgumentNullException();

        _cardsForDelete.Add(cardForDelete);

        _posibleLevelUpSlider.IncreasePossibleSliderLevelPoints(cardForDelete);
    }

    public void RetrieveCard(CardCollectionCell cardForDelete)
    {
        if (_cardsForDelete.Contains(cardForDelete) == false) throw new System.ArgumentOutOfRangeException();

        _cardsForDelete.Remove(cardForDelete);
        _posibleLevelUpSlider.DecreasePossibleSliderLevelPoints(cardForDelete);
    }
}
