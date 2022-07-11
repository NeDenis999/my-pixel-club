using Data;
using Infrastructure.Services;

public class DefenceDeck : Deck
{
    protected override void InitCards(DataSaveLoadService data)
    {
        if (data.PlayerData.DefDecks == null)
            return;
            
        for (int i = 0; i < data.PlayerData.DefDecks.Length && i < _cardsInDeck.Count; i++)
            if (data.PlayerData.DefDecks[i] != null && _cardsInDeck[i] != null)
                _cardsInDeck[i].Render(data.PlayerData.DefDecksData[i]);
    }

    protected override void SaveDecks()
    {
        var cards = new CardData[5];

        for (int i = 0; i < 5; i++) 
            cards[i] = _cardsInDeck[i].CardData;

        _data.SetDefDecks(cards);
    }
}
