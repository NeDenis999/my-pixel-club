using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Pages.Quest
{
    public enum EnemyType
    {
        Enemy,
        Boss
    }

    public class Enemy : MonoBehaviour
    {
        public event UnityAction OnEnemyDead;

        [SerializeField] private QuestFight _quest;
        [SerializeField] private EnemyType _enemyType;
    
        [SerializeField, Range(100, 1000)] 
        private int _maxHealth;
    
        [SerializeField] 
        private Image _view;
    
        private int _health;

        public float MaxHealth => _maxHealth;
        public float Health => _health;

        private void OnEnable()
        {
            _health = _maxHealth;
        }

        private void CheckAlive()
        {
            if (_health <= 0)
            {
                _health = 0;
                OnEnemyDead?.Invoke();
                Debug.Log("Enemy Dead");
            }
        }

        private int GenerateEnemyAttackValue(EnemyType enemyType)
        {
            if (enemyType == EnemyType.Enemy)
                return Random.Range(10, 50);

            if (enemyType == EnemyType.Boss)
                return Random.Range(15, 25);

            throw new System.ArgumentException();
        }

        public void TakeDamage(int amountDamage)
        {
            if (amountDamage < 0)
                throw new System.AggregateException();

            _health -= amountDamage;

            CheckAlive();
        }

        public int Damage()
        {
            if (_enemyType == EnemyType.Enemy)
                return Random.Range(10, 25);

            if (_enemyType == EnemyType.Boss)
                return Random.Range(25, 50);

            throw new System.ArgumentException();
        }
    }
}