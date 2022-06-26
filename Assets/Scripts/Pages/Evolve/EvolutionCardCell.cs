using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionCardCell : CardCell
{
    private EvolveCardCollection _evolveCardCollection;
    private CardCollectionCell _cardInCollection;

    private SelectPanel _selectPanel;

    private void Start()
    {
        _evolveCardCollection = FindObjectOfType<EvolveCardCollection>().gameObject.GetComponent<EvolveCardCollection>();
        _selectPanel = FindObjectOfType<SelectPanel>().gameObject.GetComponent<SelectPanel>();
    }

    private void OnEnable()
    {
      GetComponent<Button>().onClick.AddListener(SelectCard);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(SelectCard);
        _selectPanel.Reset();
    }

    public void SetLinkOnCardInCollection(CardCollectionCell cardInCollection)
    {
        if (cardInCollection == null) throw new System.NullReferenceException();

        _cardInCollection = cardInCollection;
    }

    private void SelectCard()
    {
        _evolveCardCollection.SelectCard(_cardInCollection);
        _selectPanel.SetPanelAboveSelectCard(this);
    }
}
