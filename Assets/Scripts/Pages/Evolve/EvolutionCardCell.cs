using System.Collections;
using System.Collections.Generic;
using Infrastructure.Services;
using Pages.Evolve;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionCardCell : CardCell
{
    [SerializeField] 
    private Button _button;

    [SerializeField] 
    private ChangingCursorHover _changingCursorHover;
    
    private CardCollectionCell _cardInCollection;
    private EvolveCardCollection _evolveCardCollection;
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

    public void Init(EvolveCardCollection evolveCardCollection, SelectPanel selectPanel, AssetProviderService assetProviderService)
    {
        if (evolveCardCollection == null || selectPanel == null)
            Debug.LogError("Ссылки не указаны");
        
        _evolveCardCollection = evolveCardCollection;
        _selectPanel = selectPanel;
        _changingCursorHover.Init(assetProviderService);
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
