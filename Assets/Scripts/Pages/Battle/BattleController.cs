using System;
using System.Collections;
using System.Collections.Generic;
using Cards.Card;
using Cards.Deck.CardCell;
using Data;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Zenject;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

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
        private Shaking shaking;

        [SerializeField] 
        private ParticleSystem _defaultAttackEffect;
        
        [SerializeField] 
        private Animator _turnEffect;
        
        [SerializeField] 
        private GameObject _battleChouse;

        private List<Card> _enemyDefCards = new();
        private int _baseEnemyDefValue;

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
            foreach (var playerCard in _playerCardAnimators) 
                playerCard.Hide();
            
            foreach (var enemyCard in _enemyCardAnimators) 
                enemyCard.Hide();

            gameObject.SetActive(true);

            HideNonAllActiveCards();
            _battleIntro.Initialization();
            StartCoroutine(Fight());
        }

        private void HideNonAllActiveCards()
        {
            HideNonActiveCards(_playerCards, _playerCardAnimators);
            HideNonActiveCards(_enemyCards, _enemyCardAnimators);
        }

        private void RenderEnemyDefCard()
        {
            _enemyCards = _enemyDefCards.ToArray();
            
            for (int i = 0; i < _enemyDefCards.Count; i++)
            {
                var card = _enemyCardAnimators[i];
                card.SetImage(_enemyDefCards[i].UIIcon);
                _enemyCardAnimators[i] = card;
                _enemyCardAnimators[i].Init(_enemyCards[i]);
            }
        }

        private IEnumerator Fight()
        {
            yield return _battleAnimator.AppearanceCards(_enemyCardAnimators, _playerCardAnimators, 
                GetAliveCards(_playerCards), GetAliveCards(_enemyCards));

            for (int i = 0; i < 2; i++)
            {
                yield return _battleIntro.SwitchTurnIntro("Player Turn");
                yield return new WaitForSeconds(0.5f);
                yield return PlayerTurn();
                yield return _battleIntro.SwitchTurnIntro("Opponent Turn");
                yield return new WaitForSeconds(0.5f);
                yield return EnemyTurn();
            }

            yield return _battleIntro.EndIntro();

            yield return new WaitForSeconds(1);
        
           if (GetAmountPlayerCardsDamage() > GetAmountEnemyCardsDef())
                OnPlayerWin?.Invoke();
            else
                OnPlayerLose?.Invoke();

            gameObject.SetActive(false);
            _battleChouse.SetActive(true);
        }

        private IEnumerator PlayerTurn()
        {
            var playerAliveCardNumbers = GetAliveCards(_playerCards);
            var enemyAliveCardNumbers = GetAliveCards(_enemyCards);

            yield return Turn(playerAliveCardNumbers, enemyAliveCardNumbers, 
                _playerCardAnimators, _enemyCardAnimators, 
                _playerCards, _enemyCards);
        }

        private IEnumerator EnemyTurn()
        {
            var playerAliveCardNumbers = GetAliveCards(_playerCards);
            var enemyAliveCardNumbers = GetAliveCards(_enemyCards);

            yield return Turn(enemyAliveCardNumbers, playerAliveCardNumbers, 
                _enemyCardAnimators, _playerCardAnimators, 
                _enemyCards, _playerCards);
        }

        private IEnumerator Turn(List<int> myAliveCardNumbers, List<int> opponentAliveCardNumbers, 
            CardAnimator[] myCardAnimators, CardAnimator[] opponentCardAnimators, Card[] myCards, Card[] opponentCards)
        {
            for (int i = 0; i < 3; i++)
            {
                var randomMyCardDamageCount = Random.Range(1, _enemyCardAnimators.Length);

                for (int j = 0; j < randomMyCardDamageCount; j++)
                {
                    var randomNumber = Random.Range(0, myAliveCardNumbers.Count);
                    Card randomMyCard = myCards[randomNumber];

                    var myCardAnimator = myCardAnimators[randomNumber];
                    myCardAnimator.Selected();
                    
                    var randomOpponentCardDamageCount = Random.Range(1, opponentCardAnimators.Length);
                    var attackEffect = randomMyCard.AttackEffect;
                    var attack = randomMyCard.Attack;
                    
                    if (IsRandomChange(randomMyCard.SkillChance))
                    {
                        foreach (var opponentCardAnimator in opponentCardAnimators)
                            StartCoroutine(opponentCardAnimator.Hit(attackEffect, attack));

                        yield return new WaitForSeconds(0.2f);
                        shaking.Shake(0.5f, 10);
                        yield return new WaitForSeconds(0.5f);
                    }
                    else
                    {
                        for (int k = 0; k < randomOpponentCardDamageCount; k++)
                        {
                            var randomOpponentCardNumber = Random.Range(0, opponentAliveCardNumbers.Count);
                            Card randomEnemyCard = opponentCards[randomOpponentCardNumber];

                            CardAnimator opponentCardAnimator = opponentCardAnimators[randomOpponentCardNumber];

                            var myAnimatorPosition = myCardAnimator.transform.position;
                            var opponentAnimatorPosition = opponentCardAnimator.transform.position;
                            
                            float angleTurnEffect = 
                                Mathf.Atan2(myAnimatorPosition.y - opponentAnimatorPosition.y, 
                                    myAnimatorPosition.x - opponentAnimatorPosition.x) * Mathf.Rad2Deg;
                            
                            var turnEffectPosition =
                                new Vector3(
                                    (myAnimatorPosition.x + opponentAnimatorPosition.x) / 2, 
                                    transform.position.y, transform.position.z);
                            
                            var turnEffect = 
                                Instantiate(_turnEffect, turnEffectPosition, new Vector3(0, 0, angleTurnEffect)
                                    .EulerToQuaternion(), transform);

                            var turnEffectImage = turnEffect.GetComponentInChildren<Image>();
                            turnEffectImage.color = Color.clear;
                            turnEffectImage.DOColor(Color.white, 0.2f);
                            
                            var ratioScale = 1f;
                            var ratioScaleRotation = (opponentAnimatorPosition.x - myAnimatorPosition.x) < 0 ? 1 : -1;
                            var scale = ratioScale * (opponentAnimatorPosition.x - myAnimatorPosition.x) *
                                        ratioScaleRotation;

                            if (Math.Abs(scale) < 1f)
                                scale = -1;
                            
                            turnEffect.transform.localScale = turnEffect.transform.localScale.ToX(scale);
                            
                            turnEffect.SetTrigger("Effect");
                            
                            StartCoroutine(opponentCardAnimator.Hit(attackEffect, attack));
                        
                            yield return new WaitForSeconds(0.2f);
                            shaking.Shake(0.5f, 10);
                            yield return new WaitForSeconds(0.1f);
                            
                            turnEffectImage.DOColor(Color.clear, 0.2f).OnComplete(()=>Destroy(turnEffect));
                        }
                    
                        yield return new WaitForSeconds(0.2f);
                    }
                }
                
                yield return new WaitForSeconds(2);
            }
        }

        private bool IsRandomChange(float change) => 
            Random.Range(0, 10000) <= (int)(change * 100);

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

        private List<int> GetAliveCards(Card[] cards)
        {
            var aliveCards = new List<int>();
            
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i].Name != "Empty")
                    aliveCards.Add(i);
            }

            return aliveCards;
        }

        private void HideNonActiveCards(Card[] cards, CardAnimator[] cardAnimators)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i].Name == "Empty")
                    cardAnimators[i].Hide();
            }
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