using System;
using System.Collections;
using System.Collections.Generic;
using Battle;
using Cards.Card;
using DG.Tweening;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

namespace FarmPage.Battle
{
    public class BattleController : MonoBehaviour
    {
        public event UnityAction OnPlayerWin;
        public event UnityAction OnPlayerLose;

        private static readonly int Effect = Animator.StringToHash("Effect");

        [SerializeField] 
        private BattleCardsStatistic _battleCardsStatistic;

        [SerializeField]
        private BattleAnimator _battleAnimator;

        [SerializeField] 
        private BattleIntro _battleIntro;

        [SerializeField]
        private CardAnimator[] _enemyCardAnimators;

        [SerializeField]
        private CardAnimator[] _playerCardAnimators;

        [SerializeField] 
        private Shaking shaking;

        [SerializeField] 
        private Animator _turnEffect;

        [SerializeField] 
        private GameObject _battleChouse;

        [SerializeField] 
        private UpPanel _upPanel;

        [SerializeField] 
        private Window _winWindow;
        
        [SerializeField]
        private Window _loseWindow;

        [SerializeField] private QuestPrizeWindow _prizeWindow;

        private List<Card> _enemyDefCards = new();
        private Card[] _enemyCards;
        private Card[] _playerCards;
        private int previousRandomNumber = -1;
        private DataSaveLoadService _dataSaveLoadService;
        private LocalDataService _localDataService;

        public RandomPrize[] _randomPrizes;

        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService, LocalDataService localDataService)
        {
            _dataSaveLoadService = dataSaveLoadService;
            _localDataService = localDataService;
        }
        
        private void Awake()
        {
            //gameObject.SetActive(false);
        }

        public void InitBattle(List<Card> enemyDefCards, RandomPrize[] randomPrizes)
        {
            _enemyDefCards = enemyDefCards;
            _randomPrizes = randomPrizes;
        }

        public void StartFight()
        {
            RenderEnemyDefCard();
            _playerCards = _dataSaveLoadService.PlayerData.AttackDecks;
            
            print(_playerCards);
            
            gameObject.SetActive(true);
            _upPanel.Block();
            
            foreach (var playerCard in _playerCardAnimators) 
                playerCard.Hide();

            foreach (var enemyCard in _enemyCardAnimators) 
                enemyCard.Hide();

            HideNonAllActiveCards();

            _battleIntro.Initialization();
            StartCoroutine(Fight());
        }

        private void HideNonAllActiveCards()
        {
            if (_playerCards == null)
                Debug.LogError("Нет карт в колоде игрока");
            
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

            for (int i = 0; i < 1; i++)
            {
                yield return _battleIntro.PlayerTurn();
                yield return new WaitForSeconds(0.5f);
                yield return PlayerTurn();
                yield return _battleIntro.OpponentTurn();
                yield return new WaitForSeconds(0.5f);
                yield return EnemyTurn();
            }
            
            if (GetAmountPlayerCardsDamage() >= GetAmountEnemyCardsDef())
                yield return PlayerWin();
            else
                yield return PlayerLose();

            _upPanel.Unblock();
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
                var randomMyCardDamageCount = _enemyCardAnimators.Length < 3 ? Random.Range(1, _enemyCardAnimators.Length) : 3;

                for (int j = 0; j < randomMyCardDamageCount; j++)
                {
                    if (previousRandomNumber != -1)
                    {
                        myCardAnimators[previousRandomNumber].Unselected();
                        yield return new WaitForSeconds(0.5f);
                    }

                    var randomNumber = myAliveCardNumbers[Random.Range(0, myAliveCardNumbers.Count)];
                    previousRandomNumber = randomNumber;
                    Card randomMyCard = myCards[randomNumber];

                    var myCardAnimator = myCardAnimators[randomNumber];
                    //myCardAnimator.transform.parent = myCardAnimator.transform.parent;
                    myCardAnimator.Selected();
                    yield return new WaitForSeconds(0.2f);

                    var randomOpponentCardDamageCount = Random.Range(1, opponentCardAnimators.Length);
                    var attackEffect = randomMyCard.AttackEffect;
                    var attack = randomMyCard.Attack;

                    if (IsRandomChange(randomMyCard.SkillChance))
                    {
                        var skillEffect = randomMyCard.SkillEffect;
                    
                        foreach (var opponentCardAnimator in opponentCardAnimators)
                            StartCoroutine(opponentCardAnimator.Hit(skillEffect, attack));

                        yield return new WaitForSeconds(0.2f);
                        shaking.Shake(0.5f, 10);
                        yield return new WaitForSeconds(0.5f);
                    }
                    else
                    {
                        for (int k = 0; k < randomOpponentCardDamageCount; k++)
                        {
                            var randomOpponentCardNumber = opponentAliveCardNumbers[Random.Range(0, opponentAliveCardNumbers.Count)];
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
                            turnEffect.SetTrigger(Effect);
                            
                            StartCoroutine(opponentCardAnimator.Hit(attackEffect, attack));
                        
                            yield return new WaitForSeconds(0.2f);
                            shaking.Shake(0.5f, 10);
                            yield return new WaitForSeconds(0.1f);
                            
                            turnEffectImage.DOColor(Color.clear, 0.2f).OnComplete(()=>Destroy(turnEffect.gameObject));
                        }
                    
                        yield return new WaitForSeconds(0.2f);
                    }
                }
            
                myCardAnimators[previousRandomNumber].Unselected();
                previousRandomNumber = -1;
                yield return new WaitForSeconds(1);
            }
        }

        private bool IsRandomChange(float change) => 
            Random.Range(0, 10000) <= (int)(change * 100);

        private int GetAmountPlayerCardsDamage()
        {
            int amountDamage = 0;

            foreach (Card cardCell in _localDataService.AttackCards)
            {
                var skillValue = 0;

                if (cardCell.Rarity != RarityCard.Empty)
                    skillValue += cardCell.TryUseSkill();

                if (skillValue != 0)
                {
                    amountDamage += skillValue;
                    //_battleCardsStatistic.AddPlayerCardWhileUsedSkill(cardCell.Name, cardCell.AttackSkillName);
                }
            }

            amountDamage += _localDataService.Attack;

            //_battleCardsStatistic.AddAmountDamage(amountDamage.ToString());

            return amountDamage;
        }

        private List<int> GetAliveCards(Card[] cards)
        {
            var aliveCards = new List<int>();
            
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i].Id != 0) 
                    aliveCards.Add(i);
            }

            return aliveCards;
        }
    
        private void HideNonActiveCards(Card[] cards, CardAnimator[] cardAnimators)
        {
            print(cards);
            print(cardAnimators);
            
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i].Id == 0)
                    cardAnimators[i].Hide();
            }
        }
        
        private int GetAmountEnemyCardsDef()
        {
            int amountDef = 0;

            foreach (var enemyCard in _enemyDefCards)
            {
                amountDef += enemyCard.Def;
                
                if (Random.Range(1, 100) == 1 && enemyCard.Rarity != RarityCard.Empty)
                {
                    amountDef += enemyCard.BonusDefSkill;
                    //_battleCardsStatistic.AddEnemyCardWhileUsedSkill(enemyCard.Name, enemyCard.AttackSkillName);
                }
            }

            return amountDef;
        }

        private IEnumerator PlayerWin()
        {
            yield return _battleIntro.EndIntro("You Win");
            yield return new WaitForSeconds(1);
            _prizeWindow.OpenPrizeWindow(_randomPrizes);
            _winWindow.ShowSmooth();
        }

        private IEnumerator PlayerLose()
        {
            yield return _battleIntro.EndIntro("You Lose");
            yield return new WaitForSeconds(1);
            _loseWindow.ShowSmooth();
        }
    }
}
