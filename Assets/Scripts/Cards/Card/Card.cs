using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RarityCard
{
    Epmpty,
    Standard,
    Rarity
}

public enum RaceCard
{
    Humans,
    Gods, 
    Demons
}

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card")]
public class Card : ScriptableObject, ICard, IRoulette
{
    [SerializeField] private Sprite _image;
    [SerializeField] private string _name;

    [SerializeField] private RarityCard _rarity;
    [SerializeField] private RaceCard _race;

    [SerializeField] private int _attack;
    [SerializeField] private int _def;
    [SerializeField] private int _health;

    [SerializeField] private string _attackSillName;
    [SerializeField] private int _attackSkill;

    [SerializeField] private string _defSkillName;
    [SerializeField] private int _defSkill;

    [Header("Skill Chance Per Procent")]
    [SerializeField] private double _skillChance;

    [SerializeField] private string _discription;

    [SerializeField] private Roulette _roulette;

    public Sprite UIIcon => _image;
    public string Name => _name;

    public RarityCard Rarity => _rarity;
    public RaceCard Race => _race;

    public int Attack => _attack;
    public int Def => _def;
    public int Health => _health;

    public int Level => 1;

    public int BonusAttackSkill => _attackSkill;
    public void TakeDamage(int damage) => _health -= damage;

    public string AttackSkillName => _attackSillName;

    public int BonusDefSkill => _defSkill;
    public string DefSkillName => _defSkillName;

    public float SkillChance => (float)_skillChance;

    public string Discription => _discription;

    Card ICard.Card => this;

    public void TakeItem()
    {
        _roulette.ReceiveCard(this);
    }
}