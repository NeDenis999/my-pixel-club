using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EnchanceCardForDeleteCell : CardCell
{
    [SerializeField] private GameObject _selectPanel;

    private EnchanceCardsForDeleteCollection _enchanceCardForDeleteCollection;
    private CardCollectionCell _cardInCollection;

    private EnhanceCardForDeleteStatistic _cardStatistic;

    private Button _cardButton;
    private Button _selectPanelButton;


    private void Awake()
    {
        _enchanceCardForDeleteCollection = FindObjectOfType<EnchanceCardsForDeleteCollection>().gameObject.GetComponent<EnchanceCardsForDeleteCollection>();

        _cardStatistic = FindObjectOfType<EnhanceCardForDeleteStatistic>().gameObject.GetComponent<EnhanceCardForDeleteStatistic>();

        _cardButton = GetComponent<Button>();
        _selectPanelButton = _selectPanel.GetComponent<Button>();
    }

    private void OnEnable()
    {
        _cardButton.onClick.AddListener(SelectCard);
        _selectPanelButton.onClick.AddListener(UnselectCard);
        _selectPanel.SetActive(false);
    }

    private void OnDisable()
    {
        _cardButton.onClick.RemoveListener(SelectCard);
        _selectPanelButton.onClick.RemoveListener(UnselectCard);
    }

    public void SetLinkOnCardInCollection(CardCollectionCell cardInCollection)
    {
        if (cardInCollection == null) throw new System.NullReferenceException();

        _cardInCollection = cardInCollection;
    }

    private void SelectCard()
    {
        _enchanceCardForDeleteCollection.AddToDeleteCollection(_cardInCollection);
        _selectPanel.SetActive(true);
        _cardStatistic.Render(_cardInCollection);
    }

    private void UnselectCard()
    {
        _enchanceCardForDeleteCollection.RetrieveCard(_cardInCollection);
        _selectPanel.SetActive(false);
    }
}
