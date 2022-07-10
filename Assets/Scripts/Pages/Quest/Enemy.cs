using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Pages.Quest
{
    public enum EnemyType
    {
        Enemy,
        Boss
    }

    public class Enemy : MonoBehaviour
    {
        [SerializeField] 
        private Image _view;
        
        private EnemyQuestData _enemyQuestData;
        private int _health;

        public float MaxHealth => _enemyQuestData.MaxHealth;
        public float Health => _health;
        public int Exp => _enemyQuestData.Exp;
        
        public void Init(EnemyQuestData enemyQuestData)
        {
            _enemyQuestData = enemyQuestData;
            _health = _enemyQuestData.MaxHealth;
            _view.sprite = enemyQuestData.View;
            _view.color = Color.white;
        }

        public void TakeDamage(int amountDamage)
        {
            if (amountDamage < 0)
                throw new System.AggregateException();

            _health -= amountDamage;

            CheckAlive();

            if (!Alive())
                Dead();
        }

        private void Dead() => 
            _view.color = new Color(0.5f, 0.5f, 0.5f, 1);

        public bool Alive() => 
            _health > 0;

        public int Damage()
        {
            return Random.Range(_enemyQuestData.Damage / 2, _enemyQuestData.Damage);
            /*
            if (_enemyType == EnemyType.Enemy)
                return Random.Range(_damage / 2, _damage);

            if (_enemyType == EnemyType.Boss)
                return Random.Range(25, 50);

            throw new System.ArgumentException();*/
        }

        private void CheckAlive()
        {
            if (_health <= 0)
            {
                _health = 0;
                Debug.Log("Enemy Dead");
            }
        }
    }
}