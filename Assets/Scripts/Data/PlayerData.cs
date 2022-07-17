using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct PlayerData
    {
        public Card[] AttackDecks;
        public Card[] DefDecks;
        public Card[] InventoryDecks;
        public CardData[] AttackDecksData;
        public CardData[] DefDecksData;
        public CardData[] InventoryDecksData;
        public int Coins;
        public int Crystals;
        public string Nickname;
        public Sprite Avatar;
        public int AvatarId;
        public float EXP;
        public float MaxExp;
        public int Energy;
        public int MaxEnergy;
        public int Level;
        public int Rank;
        public DateTime FirstDayInGame;
        public DateTime FirstDayInGame1;
        public Species Species;
        public int[] ItemsId;
        public List<ShopItemBottle> Items;
        public int CountQuestPassed;
        public int[] FarmCharacterCellIndex;
    }
}