namespace Collection
{
    public  class AttackDeck : Deck
    {
        private void Awake()
        {
            _deckType = AtackOrDefCardType.Atack;
        }
    }
}
