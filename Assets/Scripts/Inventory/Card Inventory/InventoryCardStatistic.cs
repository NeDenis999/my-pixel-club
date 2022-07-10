using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCardStatistic : MonoBehaviour
{
    [SerializeField] private TMP_Text _level, _maxAtk, _skillChance, _skillName, _effectName, _def, _health, _power, _cardName, _rare;
    [SerializeField] private Image _cardImage;

    public void Render(InventoryCardCell cardCell)
    {
        _level.text = cardCell.Level.ToString();
        _def.text = cardCell.Def.ToString();
        _health.text = cardCell.Health.ToString();
        _skillChance.text = cardCell.Card.SkillChance.ToString() + " %";
        _cardName.text = cardCell.Card.Name.ToString();
        _skillName.text = cardCell.Card.AttackSkillName.ToString();
        _rare.text = cardCell.Card.Rarity.ToString();
        _maxAtk.text = cardCell.Attack.ToString();
        _power.text = (cardCell.Attack + cardCell.Health).ToString();
        _cardImage.sprite = cardCell.Icon.sprite;
    }
}
