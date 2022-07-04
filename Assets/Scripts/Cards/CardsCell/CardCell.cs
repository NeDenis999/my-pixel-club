using Data;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public abstract class CardCell : MonoBehaviour, ICard
{
    public event UnityAction OnLevelUp;

    [SerializeField] protected Image _icon;
    protected Card _card;
    protected CardData _cardData;
    
    private int _maxLevel = 25;
    
    private float _nextMaxLevelPointMultiplier = 1.1f;
    private int _baseEnhancmentLevelPoint = 1500;

    public Image Icon => _icon;
    public int Attack => _cardData.Attack;
    public int Def => _cardData.Defence;
    public int Health => _cardData.Health;

    public int Level => _cardData.Level;
    public int Evolution => _cardData.Evolution;
    public int MaxLevel => _maxLevel;
    public float NextMaxLevelPoitnMultiplier => _nextMaxLevelPointMultiplier;

    public int BonusAttackSkill => (int)(Attack * 0.17f);
    public int Id { get; set; }

    public int LevelPoint => _cardData.LevelPoint;
    public int MaxLevelPoint => _cardData.MaxLevelPoint;
    public int AmountIncreaseLevelPoint { get; private set; }

    public virtual Card Card => _card;
    public virtual CardData CardData => _cardData;

    public int TryUseSkill()
    {
        Debug.Log(Mathf.RoundToInt(100 / Card.SkillChance).ToString());

        if (Random.Range(1, Mathf.RoundToInt(100 / Card.SkillChance)) == 1)
            return BonusAttackSkill;

        return 0;
    }

    public void Render(CardData cardData)
    {
        _cardData = cardData;
        _card = AllServices.AssetProviderService.AllCards[cardData.Id];
        
        if (cardData.Evolution == 1)
            _icon.sprite = _card.ImageFirstEvolution;
        else
            _icon.sprite = _card.ImageSecondeEvolution;
    }

    public void LevelUp(CardCell[] cardsForEnhance)
    {        
        void LevelUpCardValue()
        {
            _cardData.Attack = (int)(Attack * 1.15f);
            _cardData.Defence = (int)(Def * 1.15f);
            _cardData.Health = (int)(Health * 1.15f);
        }

        foreach (var card in cardsForEnhance)
        {
            _cardData.LevelPoint += card.GetCardDeletePoint();
            _cardData.AmountIncreaseLevelPoint += card.GetCardDeletePoint();
        }

        while (LevelPoint >= MaxLevelPoint && Level < _maxLevel)
        {
            _cardData.LevelPoint -= MaxLevelPoint;
            _cardData.MaxLevelPoint = (int)(MaxLevelPoint * _nextMaxLevelPointMultiplier);
            _cardData.Level++;
            LevelUpCardValue();
            OnLevelUp?.Invoke();

            Debug.Log("CardCell Current Level Point: " + MaxLevelPoint);
        }
    }

    public int GetCardDeletePoint()
    {
        float RacialMultiplier(RarityCard race)
        {
            float multiplier = 1;

            for (int i = 1; i < (int)race; i++)
            {
                multiplier += 0.5f;
            }

            return multiplier;
        }

        return (int)(_baseEnhancmentLevelPoint * RacialMultiplier(Card.Rarity) + _cardData.AmountIncreaseLevelPoint * 0.75f);
    }
}
