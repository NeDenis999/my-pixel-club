using System.Collections.Generic;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.Events;

public class LocalDataService
{
    public event UnityAction<int> OnLevelChange;
    public event UnityAction<int> OnEnergyChange;

    private Inventory _inventory;
    private AttackDeck _attackDeck;

    private int _health = 100;
    private int _level = 1;
    private int _energy = 25;

    private int _amountCardBaseAttack;
    private DataSaveLoadService _dataSaveLoadService;
    
    public float Health => _health;

    public int Energy => _energy;

    public int Attack
    {
        get
        {
            _amountCardBaseAttack = 0;

            foreach (var card in _dataSaveLoadService.PlayerData.AttackDecksData)
                _amountCardBaseAttack += card.Attack;

            return _amountCardBaseAttack;
        }
    }

    public Card[] AttackCards => _dataSaveLoadService.PlayerData.AttackDecks;

    public LocalDataService(DataSaveLoadService dataSaveLoadService)
    {
        _dataSaveLoadService = dataSaveLoadService;
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
        _health = MaxHealth();
    }

    public int MaxHealth()
    {
        var maxHealth = 0;

        foreach (var cardData in _dataSaveLoadService.PlayerData.AttackDecksData) 
            maxHealth += cardData.Health;

        return maxHealth;
    }
    
    private void CheakAlive()
    {
        if (_health <= 0)
        {
            _health = 0;
            Debug.Log("You Dead");
        }
    }
}
