using System.Collections;
using System.Collections.Generic;
using Pages.Battle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattle : MonoBehaviour
{
    [SerializeField] private List<Card> _enemyDefCards;
    [SerializeField] private int _amountEnemyDefValue;

    [SerializeField] private BattleConfirmWindow _battleConfirmWindow;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OpenConfirmWindow);
    }

    private void OpenConfirmWindow() => 
        _battleConfirmWindow.OpenConfirmWindow(_enemyDefCards, _amountEnemyDefValue);
}