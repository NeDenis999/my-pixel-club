using Data;

public class DefDeck : Deck
{
    private void Awake()
    { 
        _deckType = AtackOrDefCardType.Def;
    }

    protected override void InitCards(DataSaveLoadService data)
    {
        if (data.PlayerData.AttackDecks == null)
            return;
            
        for (int i = 0; i < data.PlayerData.DefDecks.Length && i < _cardsInDeck.Count; i++)
            if (data.PlayerData.DefDecks[i] != null && _cardsInDeck[i] != null)
                _cardsInDeck[i].Render(data.PlayerData.DefDecks[i]);
    }

    protected override void SaveDesks()
    {
        var Cards = new Card[_cardsInDeck.Count];

        for (int i = 0; i < _cardsInDeck.Count; i++) 
            Cards[i] = _cardsInDeck[i].Card;

        _data.SetDefDecks(Cards);
    }
}
