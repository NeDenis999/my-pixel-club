using System.Collections.Generic;
using DG.Tweening;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace Pages.Battle
{
    public class BattleConfirmWindow : MonoBehaviour
    {
        [SerializeField] private BattleController _battle;
        [SerializeField] private GameObject _exeptionBaner;
        [SerializeField] private TMP_Text _exeptionText;

        [SerializeField] 
        private CanvasGroup _canvasGroup;

        [SerializeField] 
        private GameObject _battleChouse;


        private Vector3 _startPosition;
        private Sequence _sequence;
        private List<Card> _enemyDefCards;
        private LocalDataService _localDataService;
        private DataSaveLoadService _dataSaveLoadService;
    
        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService, LocalDataService localDataService)
        {
            _dataSaveLoadService = dataSaveLoadService;
            _localDataService = localDataService;
        }
    
        private void Start()
        {
            _startPosition = transform.localPosition;
        }

        public void OpenConfirmWindow(List<Card> enemyDefCards)
        {
            _enemyDefCards = enemyDefCards;

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
                if (_dataSaveLoadService.PlayerData.Energy > 0)
                {
                    _battleChouse.SetActive(false);

                    _dataSaveLoadService.DecreaseEnergy(5);
                    _battle.SetEnemyDefCard(_enemyDefCards);
                    _battle.StartFight();
                }
                else
                {
                    _exeptionBaner.SetActive(true);
                    _exeptionText.text = "Not enough energy";
                    _dataSaveLoadService.IncreaseEnergy(25);
                }
            }
        
            gameObject.SetActive(false);
        }

        public void HideSmooth()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
        
            _sequence
                .Insert(0, DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0, 0.3f))
                .Insert(0, transform.DOLocalMove(_startPosition + new Vector3(0, -120, 0), 0.3f))
                .OnComplete(() => gameObject.SetActive(false));
        }
    
        private void ShowSmooth()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
        
            _canvasGroup.alpha = 0;
            transform.localPosition = _startPosition + new Vector3(0, 120, 0);
            _sequence
                .Insert(0, DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1, 0.6f))
                .Insert(0, transform.DOLocalMove(_startPosition, 0.5f));
        }
    
        private void OnApplicationQuit() => 
            _sequence?.Kill();
    }
}
