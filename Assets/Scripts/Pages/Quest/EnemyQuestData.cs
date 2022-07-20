using System;
using UnityEngine;

namespace FarmPage.Quest
{
    [Serializable]
    public struct EnemyQuestData
    {
        public EnemyType EnemyType;
        public Sprite View;

        [Range(100, 2000)] 
        public int MaxHealth;

        [Range(50, 500)] 
        public int Damage;

        [Range(100, 2000)] 
        public int Exp;
    }
}