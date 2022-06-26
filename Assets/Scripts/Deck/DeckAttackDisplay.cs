using System.Collections.Generic;
using Cards.Deck.CardCell;
using Data;
using UnityEngine;
using Zenject;

public class DeckAttackDisplay : MonoBehaviour
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
        print(_cardsInDeck);
        print(_cardsInDeck.Count);

        for (int i = 0; i < _cardsInDeck.Count; i++)
        {
            print(_data.PlayerData.AttackDecks[i]);
            _cardsInDeck[i].Render(_data.PlayerData.AttackDecks[i]);
        }
    }
}