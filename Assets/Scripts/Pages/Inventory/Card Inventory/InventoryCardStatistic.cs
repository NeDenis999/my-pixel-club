using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCardStatistic : MonoBehaviour
{
     [SerializeField] private TMP_Text _atk, _level, _skillChance, _skillName, _effectName, _def, _health, _power, _cardName, _rare;
    [SerializeField] private Image _cardImage;

    public void Render(InventoryCardCell cardCell)
    {
        _atk.text = cardCell.Attack.ToString();
        _def.text = cardCell.Def.ToString();
        _health.text = cardCell.Health.ToString();
        _skillChance.text = cardCell.Card.SkillChance.ToString() + " %";
        _cardName.text = cardCell.Card.Name.ToString();
        _skillName.text = cardCell.Card.AttackSkillName.ToString();
        _effectName.text = cardCell.Card.EffectName.ToString();
        _rare.text = cardCell.Card.Rarity.ToString();
        _level.text = cardCell.Level.ToString();
        _power.text = (cardCell.Attack + cardCell.Health).ToString();
        _cardImage.sprite = cardCell.Icon.sprite;
    }
}
