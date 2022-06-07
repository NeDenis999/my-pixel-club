using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefDeck : Deck
{
    private void Awake()
    { 
        _deckType = AtackOrDefCardType.Def;
    }
}
