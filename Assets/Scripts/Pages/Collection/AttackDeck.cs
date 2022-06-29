using Data;
using Infrastructure.Services;

public  class AttackDeck : Deck
{
    protected override void InitCards(DataSaveLoadService data)
    {
        if (data.PlayerData.AttackDecks == null)
            return;
            
        for (int i = 0; i < data.PlayerData.AttackDecks.Length && i < _cardsInDeck.Count; i++)
            if (data.PlayerData.AttackDecks[i] != null && _cardsInDeck[i] != null)
                _cardsInDeck[i].Render(data.PlayerData.AttackDecks[i]);
    }

    protected override void SaveDecks()
    {
        var cards = new Card[_cardsInDeck.Count];

        for (int i = 0; i < _cardsInDeck.Count; i++) 
            cards[i] = _cardsInDeck[i].Card;

        _data.SetAttackDecks(cards);
    }
}

