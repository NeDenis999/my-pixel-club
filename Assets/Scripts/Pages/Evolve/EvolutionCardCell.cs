using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionCardCell : CardCell
{
    [SerializeField] 
    private Button _button;
    
    private EvolveCardCollection _evolveCardCollection;
    private CardCollectionCell _cardInCollection;
    private SelectPanel _selectPanel;

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
        _evolveCardCollection = evolveCardCollection;
        _selectPanel = selectPanel;
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
