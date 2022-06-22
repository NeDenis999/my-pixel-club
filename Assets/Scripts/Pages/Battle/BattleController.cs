using System.Collections;
using System.Collections.Generic;
using Cards.Card;
using Cards.Deck.CardCell;
using Data;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Battle
{
    public class BattleController : MonoBehaviour
    {
        public event UnityAction OnPlayerWin;
        public event UnityAction OnPlayerLose;

        [SerializeField] private Player _player;

        [SerializeField] private CardAnimator _enemyDefCardImage;
        [SerializeField] private Transform _container;

        [SerializeField] private BattleCardsStatistic _battleCardsStatistic;

        [SerializeField]
        private BattleAnimator _battleAnimator;

        [SerializeField] 
        private BattleIntro _battleIntro;

        [SerializeField]
        private CardAnimator[] _enemyCardAnimators;
        
        [SerializeField]
        private CardAnimator[] _playerCardAnimators;
        
        private Card[] _enemyCards;
        private Card[] _playerCards;
        
        [SerializeField] 
        private ShakeCamera _shakeCamera;

        [SerializeField] 
        private ParticleSystem _defaultAttackEffect;
        
        private List<Card> _enemyDefCards = new();
        private int _baseEnemyDefValue;

        private int _countPlayerAliveCards() => PlayerAliveCards().Count;
        private int _countEnemyAliveCards() => EnemyAliveCards().Count;

        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService)
        {
            _playerCards = dataSaveLoadService.PlayerData.AttackDecks;
        }
        
        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            RenderEnemyDefCard();
        }

        public void SetEnemyDefCard(List<Card> enemyDefCards, int amountEnemyDefValue)
        {
            _enemyDefCards = enemyDefCards;
            _baseEnemyDefValue = amountEnemyDefValue;
        }

        public void StartFight()
        {
            gameObject.SetActive(true);
            _battleIntro.Initialization();
            print("Начало рпсскрытия колоды");
            StartCoroutine(Fight());
            print("Конец анимации раскрытия колоды");
        }

        private void RenderEnemyDefCard()
        {
            for (int i = 0; i < _enemyDefCards.Count; i++)
            {
                var card = _enemyCardAnimators[i];
                card.SetImage(_enemyDefCards[i].UIIcon);
                _enemyCardAnimators[i] = card;
                _enemyCardAnimators[i].Init();
            }
            
            _enemyCards = _enemyDefCards.ToArray();
        }

        private IEnumerator Fight()
        {
            yield return _battleAnimator.AppearanceCards(_enemyCardAnimators, _playerCardAnimators);

            for (int i = 0; i < 2; i++)
            {
                yield return _battleIntro.SwitchTurnIntro("Player Turn");
                yield return new WaitForSeconds(0.5f);
                yield return PlayerMove();
                yield return _battleIntro.SwitchTurnIntro("Opponent Turn");
                yield return new WaitForSeconds(0.5f);
                yield return EnemyMove();
            }

            yield return _battleIntro.EndIntro();

            yield return new WaitForSeconds(1);
        
           /* if (GetAmountPlayerCardsDamage() > GetAmountEnemyCardsDef())
                OnPlayerWin?.Invoke();
            else
                OnPlayerLose?.Invoke();
*/
            gameObject.SetActive(false);
        }

        private IEnumerator PlayerMove()
        {
            for (int i = 0; i < 3; i++)
            {
                var randomPlayerCardDamageCount = Random.Range(1, _enemyCardAnimators.Length);

                for (int j = 0; j < randomPlayerCardDamageCount; j++)
                {
                    Card randomPlayerCard = null;
                    var randomNumber = 0;

                    do
                    {
                        randomNumber = Random.Range(0, _playerCardAnimators.Length);
                        randomPlayerCard = _playerCards[randomNumber];
                    } while (randomPlayerCard.Name == "Empty");

                    var playerCardAnimator = _playerCardAnimators[randomNumber];
                    playerCardAnimator.Selected();
                    
                    var randomEnemyCardDamageCount = Random.Range(1, _enemyCardAnimators.Length);

                    for (int k = 0; k < randomEnemyCardDamageCount; k++)
                    {
                        Card randomEnemyCard = null;
                        var randomEnemyCardNumber = 0;

                        do
                        {
                            randomEnemyCardNumber = Random.Range(0, _enemyCardAnimators.Length);
                            randomEnemyCard = _enemyCards[randomEnemyCardNumber];
                        } while (randomEnemyCard.Name == "Empty");
                        
                        CardAnimator enemyCardAnimator = _enemyCardAnimators[randomEnemyCardNumber];

                        var attackEffect = randomPlayerCard.AttackEffect;
                        var attack = randomPlayerCard.Attack;

                        StartCoroutine(enemyCardAnimator.Hit(attackEffect, attack));
                        
                        yield return new WaitForSeconds(0.2f);
                        _shakeCamera.Shake(0.5f, 10);
                        yield return new WaitForSeconds(0.1f);
                    }
                    
                    yield return new WaitForSeconds(0.2f);
                }
                
                yield return new WaitForSeconds(2);
            }
        }

        private IEnumerator EnemyMove()
        {
            for (int i = 0; i < 3; i++)
            {
                var randomEnemyCardDamageCount = Random.Range(1, _enemyCardAnimators.Length);

                for (int j = 0; j < randomEnemyCardDamageCount; j++)
                {
                    var randomEnemyCard = _enemyCardAnimators[Random.Range(0, _enemyCardAnimators.Length)];
                    randomEnemyCard.Selected();
                }
                
                yield return new WaitForSeconds(0.1f);
                
                var randomPlayerCardDamageCount = Random.Range(1, _enemyCardAnimators.Length);
                
                for (int j = 0; j < randomPlayerCardDamageCount; j++)
                {
                    var randomPlayerCard = _playerCardAnimators[Random.Range(0, _playerCardAnimators.Length)];
                    var attackEffect = _defaultAttackEffect;
                    
                    if (_enemyCards[randomEnemyCardDamageCount])
                        attackEffect = _enemyCards[randomEnemyCardDamageCount].AttackEffect;
                    
                    StartCoroutine(randomPlayerCard.Hit(attackEffect, _enemyCards[randomEnemyCardDamageCount].Attack));
                    yield return new WaitForSeconds(0.2f);

                    for (int k = 0; k < randomPlayerCardDamageCount; k++)
                    {
                        _shakeCamera.Shake(0.5f, 10);
                        yield return new WaitForSeconds(0.1f);
                    }
                }
                
                yield return new WaitForSeconds(2);
            }
        }

        private int RandomNumberPlayerAliveCart() => 
            Random.Range(0, PlayerAliveCards().Count);

        private int RandomNumberEnemyAliveCart() => 
            Random.Range(0, EnemyAliveCards().Count);

        private List<ICard> PlayerAliveCards()
        {
            List<ICard> playerAliveCards = new List<ICard>();

            foreach (var playerAttackCard in _player.AttackCards)
            {
                if (playerAttackCard.Health > 0)
                    playerAliveCards.Add(playerAttackCard);
            }
        
            return playerAliveCards;
        }
    
        private List<ICard> EnemyAliveCards()
        {
            List<ICard> enemyAliveCards = new List<ICard>();

            foreach (var enemyAttackCard in _enemyDefCards)
            {
                if (enemyAttackCard.Health > 0)
                    enemyAliveCards.Add(enemyAttackCard);
            }
        
            return enemyAliveCards;
        }

        private int GetAmountPlayerCardsDamage()
        {
            int amountDamage = 0;

            foreach (CardCellInDeck cardCell in _player.AttackCards)
            {
                var skillValue = 0;

                if (cardCell.Card.Rarity != RarityCard.Epmpty)
                    skillValue += cardCell.TryUseSkill();

                if (skillValue != 0)
                {
                    amountDamage += skillValue;
                    _battleCardsStatistic.AddPlayerCardWhileUsedSkill(cardCell.Card.Name, cardCell.Card.AttackSkillName);
                }
            }

            amountDamage += _player.Attack;

            _battleCardsStatistic.AddAmountDamage(amountDamage.ToString());

            return amountDamage;
        }

        private int GetAmountEnemyCardsDef()
        {
            int amountDef = 0;

            foreach (var enemyCard in _enemyDefCards)
            {
                if (Random.Range(1, 100) == 1 && enemyCard.Rarity != RarityCard.Epmpty)
                {
                    amountDef += enemyCard.BonusDefSkill;
                    _battleCardsStatistic.AddEnemyCardWhileUsedSkill(enemyCard.Name, enemyCard.AttackSkillName);
                }
            }

            return amountDef += _baseEnemyDefValue;
        }
    }
}