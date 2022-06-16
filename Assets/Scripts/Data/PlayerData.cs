using System;
using Collection;

namespace Data
{
    [Serializable]
    public class PlayerData
    {
        public Card[] AttackDecks;
        public Card[] DefDecks;
        public int Coins;
        public int Crystals;
    }
}