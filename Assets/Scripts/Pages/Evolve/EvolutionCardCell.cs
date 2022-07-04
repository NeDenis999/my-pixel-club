using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionCardCell : CardCell
{
    [SerializeField] 
    private Button _button;

    private CardCollectionCell _cardInCollection;
    private EvolveCardCollection _evolveCardCollection;
    private SelectPanel _selectPanel;

    private bool _isInit;
    
    private void OnEnable()
    {
        _button.onClick.AddListener(SelectCard);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(SelectCard);
        
        if (_selectPanel != null)
            _selectPanel.Reset();
    }

    public void Init(EvolveCardCollection evolveCardCollection, SelectPanel selectPanel)
    {
        if (evolveCardCollection == null || selectPanel == null)
            Debug.LogError("Ссылки не указаны");
        
        _evolveCardCollection = evolveCardCollection;
        _selectPanel = selectPanel;

        _isInit = true;
    }
    
    public void SetLinkOnCardInCollection(CardCollectionCell cardInCollection)
    {
        if (cardInCollection == null) 
            throw new System.NullReferenceException();

        _cardInCollection = cardInCollection;
    }

    private void SelectCard()
    {
        _selectPanel.SetPanelAboveSelectCard(this);
        _evolveCardCollection.SelectCard(_cardInCollection);
    }
}
