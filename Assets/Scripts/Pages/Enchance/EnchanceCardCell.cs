using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnchanceCardCell : CardCell
{
    private EnchanceCardCollection _enchanceCardCollection;
    private CardCollectionCell _cardInCollection;

    private SelectPanel _selectPanel;

    private void Start()
    {
        _enchanceCardCollection = FindObjectOfType<EnchanceCardCollection>().gameObject.GetComponent<EnchanceCardCollection>();
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
        _enchanceCardCollection.SelectCard(_cardInCollection);
        _selectPanel.SetPanelAboveSelectCard(this);
    }
}
