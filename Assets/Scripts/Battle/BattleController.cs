using System.Collections;
using System.Collections.Generic;
using Cards.Card;
using Cards.Deck.CardCell;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
        
        private List<Card> _enemyDefCards = new();
        private CardAnimator[] _enemyCardAnimators;
        private int _baseEnemyDefValue;

        private int _countPlayerAliveCards() => PlayerAliveCards().Count;
        private int _countEnemyAliveCards() => EnemyAliveCards().Count;

        private void Start()
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

        public IEnumerator StartFight()
        {
            gameObject.SetActive(true);
            print("Начало рпсскрытия колоды");
            //StartCoroutine(Fight());
            StartCoroutine(_battleAnimator.AppearanceCards(_enemyCardAnimators));
            print("Конец анимации раскрытия колоды");
            //yield return new WaitForSeconds(3f);
            yield return new WaitForSeconds(1);
        }

        private void RenderEnemyDefCard()
        {
            foreach (Transform item in _container)
                Destroy(item.gameObject);

            _enemyCardAnimators = new CardAnimator[_enemyDefCards.Count];
            
            for (int i = 0; i < _enemyDefCards.Count; i++)
            {
                var card = Instantiate(_enemyDefCardImage, _container);
                card.SetImage(_enemyDefCards[i].UIIcon);
                _enemyCardAnimators[i] = card;
                _enemyCardAnimators[i].Init();
            }
        }

        private IEnumerator Fight()
        {
            yield return new WaitForSeconds(4);

            //while (_countPlayerAliveCards() > 0 && _countEnemyAliveCards() > 0)
            //{
                yield return PlayerMove();
                yield return new WaitForSeconds(1);
                //EnemyMove();
                yield return null;
            //}
        
            yield return new WaitForSeconds(1);
        
            if (GetAmountPlayerCardsDamage() > GetAmountEnemyCardsDef())
                OnPlayerWin?.Invoke();
            else
                OnPlayerLose?.Invoke();

            gameObject.SetActive(false);
        }

        private IEnumerator PlayerMove()
        {
            print("Ход игрока");
            
            /*var randomEnemyAliveCart = EnemyAliveCards()[randomNumberEnemyAliveCart];
            var randomNumberPlayerAliveCart = RandomNumberPlayerAliveCart();
            var randomPlayerAliveCart = PlayerAliveCards()[randomNumberPlayerAliveCart];*/

            //var randomNumberEnemyAliveCart = RandomNumberEnemyAliveCart();
            for (int i = 0; i < 2; i++)
                yield return _battleAnimator.HitAllCards(_enemyCardAnimators);

            StartCoroutine(_battleAnimator.HitAllCards(_enemyCardAnimators));
            yield return new WaitForSeconds(3);

            //randomEnemyAliveCart.TakeDamage(PlayerAliveCards()[randomNumberPlayerAliveCart].Attack);
        }

        private void EnemyMove()
        {
            var randomNumberEnemyAliveCart = RandomNumberEnemyAliveCart();
            var randomEnemyAliveCart = EnemyAliveCards()[randomNumberEnemyAliveCart];
            var randomNumberPlayerAliveCart = RandomNumberPlayerAliveCart();
            var randomPlayerAliveCart = PlayerAliveCards()[randomNumberPlayerAliveCart];
        
            randomPlayerAliveCart.TakeDamage(randomEnemyAliveCart.Attack);
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