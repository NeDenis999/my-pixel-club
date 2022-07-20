using Battle;
using System.Collections;
using System.Collections.Generic;
using FarmPage.Battle;
using TMPro;
using UnityEngine;

public class BattleCardsStatistic : MonoBehaviour
{
    [SerializeField] private TMP_Text _statisticText;

    [SerializeField] private BattleController _battle;

    private List<string> _listPlayerCardUsedSkill = new();
    private List<string> _listPlayerCardSkillName = new();

    private List<string> _listEnemyCardUsedSkill = new();
    private List<string> _listEnemyCardSkillName = new();

    private string _amountDamageText;

    private void OnEnable()
    {
        _battle.OnPlayerWin += RenderStatisticText;
        _battle.OnPlayerWin += RenderStatisticText;
    }

    private void OnDisable()
    {
        _listPlayerCardUsedSkill.Clear();
        _listPlayerCardSkillName.Clear();

        _listEnemyCardUsedSkill.Clear();
        _listEnemyCardSkillName.Clear();

        _battle.OnPlayerWin -= RenderStatisticText;
        _battle.OnPlayerWin -= RenderStatisticText;
    }

    public void AddPlayerCardWhileUsedSkill(string cardName, string skillName)
    {
        _listPlayerCardUsedSkill.Add(cardName);
        _listPlayerCardSkillName.Add(skillName);
    }

    public void AddEnemyCardWhileUsedSkill(string cardName, string skillName)
    {
        _listEnemyCardUsedSkill.Add(cardName);
        _listEnemyCardSkillName.Add(skillName);
    }

    public void AddAmountDamage(string amountDamage)
    {
        _amountDamageText = amountDamage;
    }

    private void RenderStatisticText()
    {
        _statisticText.text = "Amount damage: " + _amountDamageText + "\n(Battle Log)\n";

        if(_listPlayerCardUsedSkill.Count != 0)
            _statisticText.text += "Player Card:\n";

        for (int i = 0; i < _listPlayerCardUsedSkill.Count; i++)
        {
            _statisticText.text += _listPlayerCardUsedSkill[i] + " use skill: " + _listPlayerCardSkillName[i] + "\n";
        }

        if (_listEnemyCardUsedSkill.Count != 0)
            _statisticText.text += "\nEnemy card\n";

        for (int i = 0; i < _listEnemyCardUsedSkill.Count; i++)
        {
            _statisticText.text += _listEnemyCardUsedSkill[i] + " use skill: " + _listEnemyCardSkillName[i] + "\n";
        }
    }
}
