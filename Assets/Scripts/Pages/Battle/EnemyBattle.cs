using System.Collections;
using System.Collections.Generic;
using FarmPage.Battle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattle : MonoBehaviour
{
    [SerializeField] private List<Card> _enemyDefCards;

    [SerializeField] private BattleConfirmWindow _battleConfirmWindow;

    [SerializeField] private RandomPrize[] _randomPrizes;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OpenConfirmWindow);
    }

    private void OpenConfirmWindow() => 
        _battleConfirmWindow.OpenConfirmWindow(_enemyDefCards, _randomPrizes);
}