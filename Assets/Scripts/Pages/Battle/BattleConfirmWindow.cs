using System.Collections;
using System.Collections.Generic;
using Battle;
using Data;
using DG.Tweening;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using Zenject;

public class BattleConfirmWindow : MonoBehaviour
{
    [SerializeField] private BattleController _battle;

    [SerializeField] private Player _player;

    [SerializeField] private GameObject _exeptionBaner;
    [SerializeField] private TMP_Text _exeptionText;

    [SerializeField] 
    private CanvasGroup _canvasGroup;

    [SerializeField] 
    private GameObject _battleChouse;

    private Vector3 _startPosition;
    private Sequence _sequence;
    private List<Card> _enemyDefCards;
    private int _amountEnemyDefValue;
    private DataSaveLoadService _dataSaveLoadService;
    
    [Inject]
    private void Construct(DataSaveLoadService dataSaveLoadService)
    {
        _dataSaveLoadService = dataSaveLoadService;
    }
    
    private void Start()
    {
        _startPosition = transform.localPosition;
    }

    public void OpenConfirmWindow(List<Card> enemyDefCards, int amountEnemyDefValue)
    {
        _enemyDefCards = enemyDefCards;
        _amountEnemyDefValue = amountEnemyDefValue;
        _sequence = DOTween.Sequence();
        
        gameObject.SetActive(true);
        ShowSmooth();
    }

    public void OpenBattleWindow()
    {
        var isPlayerCardAlive = false;
                
        foreach (var playerCard in _dataSaveLoadService.PlayerData.AttackDecks)
        {
            if (playerCard.Name != "Empty")
                isPlayerCardAlive = true;
        }

        if (!isPlayerCardAlive)
        {
            _exeptionBaner.SetActive(true);
            _exeptionText.text = "You don't have any heroes in your deck";
        }
        else
        {
            if (_player.Energy > 0)
            {
                _battleChouse.SetActive(false);

                _player.SpendEnergy(5);
                _battle.SetEnemyDefCard(_enemyDefCards, _amountEnemyDefValue);
                _battle.StartFight();
            }
            else
            {
                _exeptionBaner.SetActive(true);
                _exeptionText.text = "Not enough energy";
            }
        }

        _sequence.Kill();
        gameObject.SetActive(false);
    }

    public void HideSmooth()
    {
        _sequence
            .Insert(0, DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0, 0.3f))
            .Insert(0, transform.DOLocalMove(_startPosition + new Vector3(0, -120, 0), 0.3f))
            .OnComplete(() =>
            {
                _sequence.Kill();
                gameObject.SetActive(false);
            });
    }
    
    private void ShowSmooth()
    {
        _canvasGroup.alpha = 0;
        transform.localPosition = _startPosition + new Vector3(0, 120, 0);
        _sequence
            .Insert(0, DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1, 0.6f))
            .Insert(0, transform.DOLocalMove(_startPosition, 0.5f));
    }
}
