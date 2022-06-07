using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleConfirmWindow : MonoBehaviour
{
    [SerializeField] private Battle _battle;

    [SerializeField] private Player _player;

    [SerializeField] private GameObject _exeptionBaner;
    [SerializeField] private TMP_Text _exeptionText;

    private List<Card> _enemyDefCards;
    private int _amountEnemyDefValue;

    public void OpenConfirmWindow(List<Card> enemyDefCards, int amountEnemyDefValue)
    {
        _enemyDefCards = enemyDefCards;
        _amountEnemyDefValue = amountEnemyDefValue;

        gameObject.SetActive(true);
    }

    public void OpenBattleWindow()
    {
        if (_player.Energy > 0)
        {
            _player.SpendEnergy(5);
            _battle.SetEnemyDefCard(_enemyDefCards, _amountEnemyDefValue);
            _battle.StartFight();
        }
        else
        {
            _exeptionBaner.SetActive(true);
            _exeptionText.text = "Not enough energy";
        }

        gameObject.SetActive(false);
    }
}
