using System.Collections.Generic;
using Cards.Deck.CardCell;
using Data;
using UnityEngine;
using Zenject;

public class DeckDeferenceDisplay : MonoBehaviour
{
    [SerializeField] 
    protected List<CardCellInDeck> _cardsInDeck;
    
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
            _cardsInDeck[i].Render(_data.PlayerData.DefDecks[i]);
    }
}