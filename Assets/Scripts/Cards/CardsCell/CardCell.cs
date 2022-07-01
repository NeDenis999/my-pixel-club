using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class CardCell : MonoBehaviour, ICard
{
    public event UnityAction OnLevelUp;

    [SerializeField] protected Image _icon;
    protected Card _card;

    private int _attack;
    private int _def;
    private int _health;
    private int _level;
    private int _maxLevel = 25;
    private int _attackSkill;

    private int _currentLevelPoint;
    private int _amountIncreaseLevelPoint;
    private int _maxLevelPoint = 1000;

    public Image Icon => _icon;
    public int Attack => _attack;
    public int Def => _def;
    public int Health => _health;
    public int Level => _level;

    public int BonusAttackSkill => _attackSkill;

    public int LevelPoint => _currentLevelPoint;
    public int MaxLevelPoint => _maxLevelPoint;

    public virtual Card Card => _card;

    public void TakeDamage(int damage) => _health -= damage;

    public int TryUseSkill()
    {
        Debug.Log(Mathf.RoundToInt(100 / Card.SkillChance).ToString());

        if (Random.Range(1, Mathf.RoundToInt(100 / Card.SkillChance)) == 1)
            return _attackSkill;

        return 0;
    }

    public void Render(ICard card)
    {
        if (card == null) throw new System.ArgumentNullException();

        _card = card is Card ? (Card)card : (card as CardCell).Card;

        _icon.sprite = _card.UIIcon;
        _attack = card.Attack;
        _def = card.Def;
        _health = card.Health;
        _level = card.Level;
        _attackSkill = card.BonusAttackSkill;
    }

    public void LevelUp(CardCell[] cardsForEnhance)
    {
        float RacialMultiplier(RarityCard race)
        {
            float multiplier = 1;

            for (int i = 1; i < (int)race; i++)
            {
                multiplier += 0.5f;
            }

            return multiplier;
        }
        void LevelUpCardValue()
        {
            _attack = (int)(_attack * 1.15f);
            _def = (int)(_def * 1.15f);
            _health = (int)(_health * 1.15f);
        }

        foreach (var card in cardsForEnhance)
        {
            int deleteCardPoint = (int)(100 * RacialMultiplier(card.Card.Rarity) + _amountIncreaseLevelPoint * 0.75f);

            _currentLevelPoint += deleteCardPoint;
            _amountIncreaseLevelPoint += deleteCardPoint;

        }

        while (_currentLevelPoint >= _maxLevelPoint && _level < _maxLevel)
        {
            _currentLevelPoint -= _maxLevelPoint;
            _maxLevelPoint = (int)(_maxLevelPoint * 1.1f);
            _level++;
            LevelUpCardValue();
            OnLevelUp?.Invoke();
        }
    }
}
