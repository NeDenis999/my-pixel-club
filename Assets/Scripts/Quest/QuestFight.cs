using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestFight : MonoBehaviour
{
    public event UnityAction OnPlayerWin;
    public event UnityAction OnPlayerLose;

    [SerializeField] private Slider _enemyHealthSlider, _playerHealthSlider, _playerExpSlider;

    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;

    [SerializeField] private TMP_Text _enemyHelthPerProcentText, _playerHealthPerProcentText, _playerExpPerProcentText;

    [SerializeField] private QuestConfirmWindow _questConfirmWindow;

    [SerializeField] private GameObject _questList;

    private bool _isFight;

    private void Start()
    {
        _playerHealthSlider.maxValue = _player.MaxHealth;        
        _enemyHealthSlider.maxValue = _enemy.MaxHealth;
    }

    private void OnEnable()
    {
        _playerExpSlider.maxValue = _player.MaxExp;
        _playerExpSlider.value = _player.Exp;

        _playerHealthSlider.value = _player.Health;
        _playerHealthPerProcentText.text = (_player.Health / _player.MaxHealth * 100).ToString() + " %"; ;

        _enemyHealthSlider.value = _enemy.MaxHealth;
        _enemyHelthPerProcentText.text = "100 %";

        _playerExpPerProcentText.text = (_player.Exp / _player.MaxExp * 100).ToString() + " %";

        StartCoroutine(StartFight());
    }

    private IEnumerator StartFight()
    {
        _player.SpendEnergy(_questConfirmWindow.RequiredAmountEnergy);

        _isFight = true;

        yield return new WaitForSeconds(0.5f);

        while (_isFight)
        {
            HitEnemy();

            yield return new WaitForSeconds(1);

            if (_enemy.Health <= 0)
                _isFight = false;
            else
                HitPlayer();

            yield return new WaitForSeconds(0.5f);
        }
        
        gameObject.SetActive(false);
        _questList.SetActive(true);
        
        if (_player.Health > 0)
        {
            OnPlayerWin?.Invoke();
            _player.GetExp(25);
            _playerExpPerProcentText.text = (_player.Exp / _player.MaxExp * 100).ToString() + " %";
        }
    }

    private int GenerateEnemyAttackValue(EnemyType enemyType)
    {
        if (enemyType == EnemyType.Enemy)
            return Random.Range(1, 5);

        if (enemyType == EnemyType.Boss)
            return Random.Range(15, 25);

        throw new System.ArgumentException();
    }  

    private void HitPlayer()
    {
        _player.TakeDamage(GenerateEnemyAttackValue(_enemy.TypeEnemy));

        _playerHealthSlider.value = _player.Health;
        _playerHealthPerProcentText.text = (_player.Health / _player.MaxHealth * 100).ToString() + " %";

        if (_player.Health <= 0)
        {
            _isFight = false;
            OnPlayerLose?.Invoke();
        }
    }

    private void HitEnemy()
    {
        _enemy.TakeDamage(_player.Attack);
        _enemyHealthSlider.value = _enemy.Health;
        _enemyHelthPerProcentText.text = (_enemy.Health / _enemy.MaxHealth * 100).ToString() + " %";
    }
}
