using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionCardCell : CardCell
{
    private EvolveCardCollection _evolveCardCollection;
    private CardCollectionCell _cardInCollection;

    private void Start()
    {
        _evolveCardCollection = FindObjectOfType<EvolveCardCollection>().gameObject.GetComponent<EvolveCardCollection>();
    }

    private void OnEnable()
    {
      GetComponent<Button>().onClick.AddListener(SelectCard);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(SelectCard);
    }

    public void SetLinkOnCardInCollection(CardCollectionCell cardInCollection)
    {
        if (cardInCollection == null) throw new System.NullReferenceException();

        _cardInCollection = cardInCollection;
    }

    private void SelectCard()
    {
        _evolveCardCollection.SelectCard(_cardInCollection);
    }
}
