using System;
using UnityEngine;

namespace Pages.Quest
{
    [Serializable]
    public struct EnemyQuestData
    {
        public EnemyType EnemyType;
        public Sprite View;

        [Range(100, 1000)] 
        public int MaxHealth;

        [Range(25, 100)] 
        public int Damage;

        [Range(10, 1000)] 
        public int Exp;
    }
}