using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField] private int _maxHealth;
    private int _health;

    public float MaxHealth => _maxHealth;
    public float Health => _health;
    public EnemyType TypeEnemy => _enemyType;

    private void OnEnable()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int amountDamage)
    {
        if (amountDamage < 0)
            throw new System.AggregateException();

        _health -= amountDamage;

        CheckAlive();
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
}
