using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{
    public event UnityAction OnPlayerWin;
    public event UnityAction OnPlayerLose;

    [SerializeField] private Player _player;

    [SerializeField] private Image _enemyDefCardImage;
    [SerializeField] private Transform _container;

    [SerializeField] private BattleCardsStatistic _battleCardsStatistic;

    private List<Card> _enemyDefCards = new();

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

    public void StartFight()
    {
        gameObject.SetActive(true);
        StartCoroutine(Fight());
    }

    private void RenderEnemyDefCard()
    {
        foreach (Transform item in _container)
            Destroy(item.gameObject);

        for (int i = 0; i < _enemyDefCards.Count; i++)
        {
            var card = Instantiate(_enemyDefCardImage, _container);
            card.sprite = _enemyDefCards[i].UIIcon;
        }
    }

    private IEnumerator Fight()
    {
        yield return new WaitForSeconds(1);

        while (_countPlayerAliveCards() > 0 && _countEnemyAliveCards() > 0)
        {
            PlayerMove();
            yield return new WaitForSeconds(1);
            EnemyMove();
            yield return null;
        }
        
        yield return new WaitForSeconds(1);
        
        if (GetAmountPlayerCardsDamage() > GetAmountEnemyCardsDef())
            OnPlayerWin?.Invoke();
        else
            OnPlayerLose?.Invoke();

        gameObject.SetActive(false);
    }

    private void PlayerMove()
    {
        var randomEnemyAliveCart = RandomEnemyAliveCart();
        var randomPlayerAliveCart = RandomPlayerAliveCart();
        
        randomEnemyAliveCart.TakeDamage(randomPlayerAliveCart.Attack);
    }

    private void EnemyMove()
    {
        var randomEnemyAliveCart = RandomEnemyAliveCart();
        var randomPlayerAliveCart = RandomPlayerAliveCart();
        
        randomPlayerAliveCart.TakeDamage(randomEnemyAliveCart.Attack);
    }

    private ICard RandomPlayerAliveCart() => 
        PlayerAliveCards()[Random.Range(0, PlayerAliveCards().Count)];

    private ICard RandomEnemyAliveCart() => 
        EnemyAliveCards()[Random.Range(0, EnemyAliveCards().Count)];

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