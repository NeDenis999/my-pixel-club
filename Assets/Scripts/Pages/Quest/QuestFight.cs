using System.Collections;
using System.Security.Cryptography;
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
        
        [SerializeField] private TMP_Text _enemyHelthPerProcentText, _playerHealthPerProcentText, _playerExpPerProcentText;
        [SerializeField] private QuestConfirmWindow _questConfirmWindow;
        [SerializeField] private GameObject _questList;
        [SerializeField] private QuestPrizeWindow _winWindow;
        [SerializeField] private GameObject _loseWindow;
        [SerializeField] private Image _playerIcon;

        [SerializeField]
        private Slider _experienceBeforeSlider;

        [SerializeField]
        private SliderAnimator _experienceSliderAnimator;

        [SerializeField]
        private SliderAnimator _enemyHealthSliderAnimator;
        
        [SerializeField]
        private SliderAnimator _healthSliderAnimator;
        
        [SerializeField] 
        private Shaking _shaking;

        [SerializeField]
        private ParticleSystem _attackEffect;

        [SerializeField] 
        private Transform _container;
        
        [SerializeField] 
        private Enemy _enemyPrefab;
        
        private LocalDataService _localDataService;
        private DataSaveLoadService _dataSaveLoadService;
        private AssetProviderService _assetProviderService;
        private bool _isFight;

        private Enemy[] _enemies;
        private Chapter _chapter;

        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService, AssetProviderService assetProviderService, LocalDataService localDataService)
        {
            _dataSaveLoadService = dataSaveLoadService;
            _assetProviderService = assetProviderService;
            _localDataService = localDataService;
        }

        public void StartFight(Chapter chapter)
        {
            _chapter = chapter;
            _enemies = new Enemy[chapter.EnemyQuestsData.Length];

            for (int i = 0; i < _enemies.Length; i++)
            {
                _enemies[i] = Instantiate(_enemyPrefab, _container);
                _enemies[i].Init(chapter.EnemyQuestsData[i]);
            }
            
            _healthSliderAnimator.Slider.maxValue = _localDataService.MaxHealth();        
            _enemyHealthSliderAnimator.Slider.maxValue = EnemiesMaxHealth();
            gameObject.SetActive(true);
            InitFight();
            StartCoroutine(Fight());
        }

        private void InitFight()
        {
            _playerIcon.sprite = _dataSaveLoadService.PlayerData.Avatar;

            _experienceSliderAnimator.Slider.maxValue = _dataSaveLoadService.PlayerData.MaxExp;
            _experienceSliderAnimator.Slider.value = _dataSaveLoadService.PlayerData.EXP;
            _playerExpPerProcentText.text = (_dataSaveLoadService.PlayerData.EXP / _dataSaveLoadService.PlayerData.MaxExp * 100).ToString() + " %";
            _experienceBeforeSlider.maxValue = _dataSaveLoadService.PlayerData.MaxExp;
            _experienceBeforeSlider.value = 0;
            
            _healthSliderAnimator.Slider.value = _localDataService.Health;
            _playerHealthPerProcentText.text = (_localDataService.Health / _localDataService.MaxHealth() * 100).ToString() + " %"; ;

            _enemyHealthSliderAnimator.Slider.value = EnemiesMaxHealth();
            _enemyHelthPerProcentText.text = "100 %";
                    
            _dataSaveLoadService.DecreaseEnergy(_questConfirmWindow.RequiredAmountEnergy);
        }
        
        private IEnumerator Fight()
        {
            _isFight = true;

            yield return new WaitForSeconds(0.5f);

            while (_isFight)
            {
                var randomEnemy = _enemies[Random.Range(0, _enemies.Length)];
                
                if (randomEnemy.Alive()) 
                    TurnPlayer(randomEnemy);
                else
                {
                    foreach (var enemy in _enemies)
                    {
                        if (enemy.Health > 0)
                        {
                            TurnPlayer(enemy);
                            break;
                        }
                    }
                }


                yield return new WaitForSeconds(1);

                if (EnemiesHealth() <= 0)
                    _isFight = false;
                else
                    TurnEnemies();
                
                yield return new WaitForSeconds(0.5f);
            }

            //yield return new WaitForSeconds(0.5f);

            if (IsAlive())
                yield return PlayerWin();
            else
                yield return PlayerLose();

            DestroyAlllEnemies();
            
            gameObject.SetActive(false);
            _questList.SetActive(true);
            _localDataService.RevertHealth();
        }

        private void DestroyAlllEnemies()
        {
            foreach (var enemy in _enemies)
                Destroy(enemy.gameObject);
        }

        private IEnumerator PlayerLose()
        {
            yield return new WaitForSeconds(1f);
            _loseWindow.SetActive(true);

            //OnPlayerLose?.Invoke();
        }

        private IEnumerator PlayerWin()
        {
            _dataSaveLoadService.IncreaseEXP(EnemiesExp());
            _playerExpPerProcentText.text =
                (_dataSaveLoadService.PlayerData.EXP / _dataSaveLoadService.PlayerData.MaxExp * 100).ToString() + " %";

            if (_dataSaveLoadService.PlayerData.EXP > _experienceSliderAnimator.Slider.value)
            {
                _experienceBeforeSlider.value = _dataSaveLoadService.PlayerData.EXP;
                yield return new WaitForSeconds(0.5f);
                
                _experienceSliderAnimator.UpdateSlider(_dataSaveLoadService.PlayerData.EXP, 
                    _dataSaveLoadService.PlayerData.MaxExp, 1, _experienceSliderAnimator.Slider.value);
            }
            else
            {
                print("Exp > Slider exp");
                _playerExpPerProcentText.text = "Level Up";
                _experienceBeforeSlider.value = _dataSaveLoadService.PlayerData.MaxExp;
                yield return new WaitForSeconds(0.5f);
                
                _experienceSliderAnimator.UpdateSlider(_dataSaveLoadService.PlayerData.MaxExp,
                    _dataSaveLoadService.PlayerData.MaxExp, 1, _experienceSliderAnimator.Slider.value);
                //yield return new WaitForSeconds(1f);

                /*_experienceSliderAnimator.UpdateSlider(_dataSaveLoadService.PlayerData.EXP,
                    _dataSaveLoadService.PlayerData.MaxExp, 1, 0);*/
            }

            yield return new WaitForSeconds(2f);
            _winWindow.OpenPrizeWindow();
            _chapter.NextChapter.UnlockedChapter();
            
            //OnPlayerWin?.Invoke();
        }

        private bool IsAlive() => 
            _localDataService.Health > 0;

        private void TurnPlayer(Enemy enemy)
        {
            enemy.TakeDamage(_localDataService.Attack); //1
            var effect = Instantiate(_attackEffect, enemy.transform.position, Quaternion.identity);
            _shaking.Shake(0.5f, 10);
            Destroy(effect.gameObject, 4);
            _enemyHealthSliderAnimator.UpdateSlider(EnemiesHealth(), EnemiesMaxHealth(), 1, _enemyHealthSliderAnimator.Slider.value);
            _enemyHelthPerProcentText.text = (EnemiesHealth() / EnemiesMaxHealth() * 100).ToString() + " %";
        }
                
        private void TurnEnemies()
        {
            _localDataService.TakeDamage(EnemiesDamage());
            _shaking.Shake(0.5f, 10);
            _healthSliderAnimator.UpdateSlider(_localDataService.Health, _localDataService.MaxHealth(), 1, _healthSliderAnimator.Slider.value);
            _playerHealthPerProcentText.text = (_localDataService.Health / _localDataService.MaxHealth() * 100).ToString() + " %";

            if (_localDataService.Health <= 0) 
                _isFight = false;
        }

        private float EnemiesHealth()
        {
            var health = 0f;

            foreach (var enemy in _enemies) 
                health += enemy.Health;

            return health;
        }
        
        private float EnemiesMaxHealth()
        {
            var maxHealth = 0f;

            foreach (var enemy in _enemies) 
                maxHealth += enemy.MaxHealth;

            return maxHealth;
        }
        
        private int EnemiesExp()
        {
            var exp = 0;

            foreach (var enemy in _enemies) 
                exp += enemy.Exp;

            return exp;
        }
        
        private int EnemiesDamage()
        {
            var damage = 0;

            foreach (var enemy in _enemies) 
                damage += enemy.Damage();

            return damage / _enemies.Length;
        }
    }
}
