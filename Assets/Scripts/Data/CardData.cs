using System;

namespace Data
{
    [Serializable]
    public struct CardData
    {
        public int Id;
        public int Level;
        public int Evolution;
        public int Attack;
        public int Defence;
        public int Health;
        public int LevelPoint;
        public int MaxLevelPoint;
        public int AmountIncreaseLevelPoint;
    }
}