using Data;
using Zenject;

namespace Collection
{
    public  class AttackDeck : Deck
    {
        [Inject]
        public void Construct(PlayerDataScriptableObject data)
        {
            for (int i = 0; i < data.PlayerData.AttackDecks.Length && i < _cardsInDeck.Count; i++)
                if (data.PlayerData.AttackDecks[i] != null && _cardsInDeck[i] != null)
                    _cardsInDeck[i].Render(data.PlayerData.AttackDecks[i]);
        }
        
        private void Awake()
        {
            _deckType = AtackOrDefCardType.Atack;
        }
    }
}
