using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class CardCell : MonoBehaviour, ICard
{
    [SerializeField] private Image _icon;
    [SerializeField] private Card _card;

    private int _attack;
    private int _def;
    private int _health;
    private int _level;
    private int _attackSkill;
   
    public int Attack => _attack;
    public int Def => _def;
    public int Health => _health;
    public int Level => _level;

    public int BonusAttackSkill => _attackSkill;
    public void TakeDamage(int damage) => _health -= damage;

    public virtual Card Card => _card;

    public int TryUseSkill()
    {
        Debug.Log(Mathf.RoundToInt(100 / Card.SkillChance).ToString());

        if (Random.Range(1, Mathf.RoundToInt(100 / Card.SkillChance)) == 1)
            return _attackSkill;

        return 0;
    }

    public void Render(Card cardForRender)
    {
        CopyCardValue(cardForRender);        
    }

    public void SwitchComponentValue(CardCell cardCell)
    {
        CopyCardValue(cardCell);
    }

    private void CopyCardValue<T>(T cardCell) where T : ICard
    {
        _card = cardCell.Card;

        _icon.sprite = _card.UIIcon;
        _attack = cardCell.Attack;
        _def = cardCell.Def;
        _level = cardCell.Level;
        _attackSkill = cardCell.BonusAttackSkill;  
    }
}
