using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EnchanceCardForDeleteCell : CardCell
{
    private EnchanceCardsForDeleteCollection _enchanceCardForDeleteCollection;
    private CardCollectionCell _cardInCollection;

    private GameObject _selectPanel;

    private bool _isSelect;

    private void Start()
    {
        _enchanceCardForDeleteCollection = FindObjectOfType<EnchanceCardsForDeleteCollection>().gameObject.GetComponent<EnchanceCardsForDeleteCollection>();
    }

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(SelectCard);
        GetComponent<Button>().onClick.AddListener(UnselectCard);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(SelectCard);
        GetComponent<Button>().onClick.RemoveListener(UnselectCard);
        _selectPanel.SetActive(false);
    }

    public void SetLinkOnCardInCollection(CardCollectionCell cardInCollection)
    {
        if (cardInCollection == null) throw new System.NullReferenceException();

        _cardInCollection = cardInCollection;
    }

    private void SelectCard()
    {
        if (_isSelect == true) return;

        _enchanceCardForDeleteCollection.AddToDeleteCollection(_cardInCollection);
        _selectPanel.SetActive(true);
        _isSelect = true;
    }

    private void UnselectCard()
    {
        if (_isSelect == false) return;

        _enchanceCardForDeleteCollection.RetrieveCard(_cardInCollection);
        _selectPanel.SetActive(false);
        _isSelect = false;
    }
}
