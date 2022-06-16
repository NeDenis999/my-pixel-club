using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using Zenject;

public class DefDeck : Deck
{
    [Inject]
    public void Construct(PlayerDataScriptableObject data)
    {
        for (int i = 0; i < data.PlayerData.DefDecks.Length && i < _cardsInDeck.Count; i++)
            if (data.PlayerData.DefDecks[i] != null && _cardsInDeck[i] != null)
                _cardsInDeck[i].Render(data.PlayerData.DefDecks[i]);
    }
    
    private void Awake()
    { 
        _deckType = AtackOrDefCardType.Def;
    }
}
