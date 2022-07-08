using System.Collections;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Pages.Quest
{
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
        [SerializeField] private QuestPrizeWindow _winOrLoseWindow;
        [SerializeField] private Image _playerIcon;

        [SerializeField]
        private SliderAnimator _experienceSliderAnimator;
        
        [SerializeField] 
        private Shaking _shaking;

        [SerializeField]
        private ParticleSystem _attackEffect;
        
        private DataSaveLoadService _dataSaveLoadService;
        private AssetProviderService _assetProviderService;
        private bool _isFight;

        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService, AssetProviderService assetProviderService)
        {
            _dataSaveLoadService = dataSaveLoadService;
            _assetProviderService = assetProviderService;
        }
        
        private void OnEnable()
        {
            _playerHealthSlider.maxValue = _player.MaxHealth;        
            _enemyHealthSlider.maxValue = _enemy.MaxHealth;
        }

        private void InitFight()
        {
            _playerIcon.sprite = _dataSaveLoadService.PlayerData.Avatar;
            _playerExpSlider.maxValue = _player.MaxExp;
            _playerExpSlider.value = _dataSaveLoadService.PlayerData.EXP;

            _playerHealthSlider.value = _player.Health;
            _playerHealthPerProcentText.text = (_player.Health / _player.MaxHealth * 100).ToString() + " %"; ;

            _enemyHealthSlider.value = _enemy.MaxHealth;
            _enemyHelthPerProcentText.text = "100 %";

            _playerExpPerProcentText.text = (_dataSaveLoadService.PlayerData.EXP / _player.MaxExp * 100).ToString() + " %";
        
            _dataSaveLoadService.DecreaseEnergy(_questConfirmWindow.RequiredAmountEnergy);
        }
        
        private IEnumerator Fight()
        {
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

            if (_player.Health > 0)
            {
                _dataSaveLoadService.IncreaseEXP(25);
                _playerExpPerProcentText.text = (_dataSaveLoadService.PlayerData.EXP / _player.MaxExp * 100).ToString() + " %";
                _experienceSliderAnimator.UpdateSlider(_dataSaveLoadService.PlayerData.EXP, _player.MaxExp);
                yield return new WaitForSeconds(2f);

                _winOrLoseWindow.OpenPrizeWindow();
            }

            gameObject.SetActive(false);
            _questList.SetActive(true);
            _player.RevertHealth();
        }

        public void StartFight()
        {
            gameObject.SetActive(true);
            InitFight();
            StartCoroutine(Fight());
        }
        
        private void HitPlayer()
        {
            _player.TakeDamage(_enemy.Damage());
            _shaking.Shake(0.5f, 10);
            _playerHealthSlider.value = _player.Health;
            _playerHealthPerProcentText.text = (_player.Health / _player.MaxHealth * 100).ToString() + " %";

            if (_player.Health <= 0)
            {
                _isFight = false;
            }
        }

        private void HitEnemy()
        {
            _enemy.TakeDamage(_player.Attack); //1
            var effect = Instantiate(_attackEffect, _enemy.transform.position, Quaternion.identity);
            _shaking.Shake(0.5f, 10);
            Destroy(effect, 4);
            _enemyHealthSlider.value = _enemy.Health;
            _enemyHelthPerProcentText.text = (_enemy.Health / _enemy.MaxHealth * 100).ToString() + " %";
        }
    }
}
