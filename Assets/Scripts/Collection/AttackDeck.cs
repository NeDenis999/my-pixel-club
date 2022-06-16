using Data;
using Zenject;

namespace Collection
{
    public  class AttackDeck : Deck
    {
        private void Awake()
        {
            _deckType = AtackOrDefCardType.Atack;
        }

        protected override void InitCards(DataSaveLoadService data)
        {
            if (data.PlayerData.AttackDecks == null)
                return;
            
            for (int i = 0; i < data.PlayerData.AttackDecks.Length && i < _cardsInDeck.Count; i++)
                if (data.PlayerData.AttackDecks[i] != null && _cardsInDeck[i] != null)
                    _cardsInDeck[i].Render(data.PlayerData.AttackDecks[i]);
        }

        protected override void SaveDesks()
        {
            var Cards = new Card[_cardsInDeck.Count];

            for (int i = 0; i < _cardsInDeck.Count; i++) 
                Cards[i] = _cardsInDeck[i].Card;

            _data.SetAttackDecks(Cards);
        }
    }
}
