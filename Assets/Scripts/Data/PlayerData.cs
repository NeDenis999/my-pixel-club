using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct PlayerData
    {
        public Card[] AttackDecks;
        public Card[] DefDecks;
        public Card[] InventoryDecks;
        public int Coins;
        public int Crystals;
        public string Nickname;
        public Sprite Avatar;
        public float XP;
        public float Energy;
        public int Level;
        public int Rank;
        public DateTime FirstDayInGame;
    }
}