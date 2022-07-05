using System.Collections;
using System.Collections.Generic;
using Cards.CardCell;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Player : MonoBehaviour
{
    public event UnityAction<int> OnLevelChange;
    public event UnityAction<int> OnEnergyChange;

    [SerializeField] private Inventory _inventory;
    [SerializeField] private AttackDeck _attackDeck;
    [SerializeField] private int _maxHealth = 100;
    
    private int _health = 100;
    private int _level = 1;
    private int _energy = 25;
    private int _exp = 0;

    private int _amountCardBaseAttack;
    private int _amountCardBaseDef;

    public float MaxHealth => _maxHealth;
    public float Health => _health;

    public float MaxExp => _level * 100;
    public float Exp => _exp;

    public int Energy => _energy;

    public int Attack => _amountCardBaseAttack;
    public int Def => _amountCardBaseDef;

    public List<CardCellInDeck> AttackCards => _attackDeck.CardsInDeck;

    private void OnEnable()
    {
        foreach (var item in _attackDeck.CardsInDeck)
            _amountCardBaseAttack += item.Attack;

        _attackDeck.OnCardChanged += (List<CardCellInDeck> cardInDeck) =>
        {
            _amountCardBaseAttack = 0;

            foreach (var item in cardInDeck)
                _amountCardBaseAttack += item.Attack;
        };

        _inventory.OnReestablishEnergy += () =>
        {
            _energy = 25;
            OnEnergyChange?.Invoke(_energy);
        };
    }

    public void SpendEnergy(int energy)
    {
        if (energy > _energy)
            throw new System.ArgumentOutOfRangeException();

        _energy -= energy;
    }

    public void TakeDamage(int amountDamage)
    {
        if (amountDamage < 0)
            throw new System.ArgumentException();

        _health -= amountDamage;
        CheakAlive();
    }

    public void RevertHealth()
    {
        _health = _maxHealth;
    }

    public void GetExp(int exp)
    {
        _exp += exp;
        CheckLevelUp();
    }

    private void CheakAlive()
    {
        if (_health <= 0)
        {
            _health = 0;
            Debug.Log("You Dead");
        }
    }

    private void CheckLevelUp()
    {
        if (_exp >= MaxExp)
        {
            _level++;
            _exp = 0;
            OnLevelChange?.Invoke(_level);
        }
    }
}
