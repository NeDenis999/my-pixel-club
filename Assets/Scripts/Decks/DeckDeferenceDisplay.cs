using System.Collections.Generic;
using Cards.CardCell;
using Cards.CardsCell;
using Data;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

public class DeckDeferenceDisplay : MonoBehaviour
{
    [SerializeField] 
    protected List<CardDisplay> _cardsInDeck;
    
    private DataSaveLoadService _data;
    
    [Inject]
    public void Construct(DataSaveLoadService data)
    {
        _data = data;
        UpdateCardDisplay();
    }

    private void OnEnable()
    {
        UpdateCardDisplay();
    }

    private void UpdateCardDisplay()
    {
        for (int i = 0; i < _cardsInDeck.Count; i++) 
            _cardsInDeck[i].UpdateDisplay(_data.PlayerData.DefDecks[i]);
    }
}