using Infrastructure.Services;
using Roulette;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum RarityCard
{
    Standard,
    Rarity,
    Epmpty
}

public enum RaceCard
{
    Humans,
    Gods, 
    Demons
}

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card")]
public class Card : ScriptableObject, ICard, IRoulette
{
    private const float ValueIncreaseMultiplier = 1.35f;
    
    [SerializeField] private Sprite _currentImage;

    [SerializeField] private Sprite _imageFirstEvolution;
    [SerializeField] private Sprite _imageSecondeEvolution;
    private int _evolution = 1;
    public int Evolution => _evolution;

    [SerializeField] private string _name;

    [SerializeField] private RarityCard _rarity;
    [SerializeField] private RaceCard _race;

    [SerializeField] private int _attack;
    [SerializeField] private int _def;
    [SerializeField] private int _health;
    private int _level = 1;

    [SerializeField] private string _attackSillName;
    [SerializeField] private int _attackSkill;

    [SerializeField] private string _defSkillName;
    [SerializeField] private int _defSkill;

    [Header("Skill Chance Per Procent")]
    [SerializeField] private double _skillChance;

    [SerializeField] private string _discription;

    [SerializeField] private Vector2 _directionView;

    [SerializeField] 
    private ParticleSystem _attackEffect;

    [SerializeField] 
    private Image _attackIcon;

    public Sprite UIIcon
    {
        get
        {
            if (_evolution < 2)
                return _imageFirstEvolution;
            else
                return _imageSecondeEvolution;
        }
    }

    public string Name => _name;

    public RarityCard Rarity => _rarity;
    public RaceCard Race => _race;

    public int Attack
    {
        get
        {
            if (_attack < 2)
                return _attack;
            
            return (int)(_attack * ValueIncreaseMultiplier);
        }
    }

    public int Def
    {
        get
        {
            if (_evolution < 2)
                return _def;
            
            return (int)(_def * ValueIncreaseMultiplier);
        }
    }

    public int Health
    {
        get
        {
            if (_evolution < 2)
                return _health;
            
            return (int)(_health * ValueIncreaseMultiplier);
        }
    }

    public int Level => _level;
    public Vector2 DirectionView => _directionView;
    public ParticleSystem AttackEffect => _attackEffect;
    public Image AttackIcon => _attackIcon;

    public int BonusAttackSkill => _attackSkill;
    public int Id { get; set; }
    public void TakeDamage(int damage) => _health -= damage;

    public string AttackSkillName => _attackSillName;

    public int BonusDefSkill => _defSkill;
    public string DefSkillName => _defSkillName;

    public float SkillChance => (float)_skillChance;

    public string Discription => _discription;

    public Sprite ImageFirstEvolution => _imageFirstEvolution;
    public Sprite ImageSecondeEvolution => _imageSecondeEvolution;
    
    Card ICard.Card => this;

    public void Init(int evolution, int level, int id)
    {
        _evolution = evolution;
        _level = level;
        Id = id;
    }
    
    public void Evolve(EvolutionCard firstCard, EvolutionCard secondCard)
    {
        if (firstCard.CardCell.UIIcon != secondCard.CardCell.UIIcon || _evolution == 2) throw new System.InvalidOperationException();

        int GetLevelUpValue(int firstValue, int secondValue)
        {
            return (int)((firstValue + secondValue) / 2 * ValueIncreaseMultiplier);
        }

        //_attack = GetLevelUpValue(firstCard.CardCell.Attack, secondCard.CardCell.Attack);
        //_def = GetLevelUpValue(firstCard.CardCell.Def, secondCard.CardCell.Def);
        //_health = GetLevelUpValue(firstCard.CardCell.Health, secondCard.CardCell.Health);
        Id = firstCard.CardCell.Id;
        
        _evolution++;
    }

    public void TakeItem()
    {
        var roulettePage = FindObjectOfType<RoulettePage>().gameObject.GetComponent<RoulettePage>();

        roulettePage.AccrueCard(this);
    }

    public Sprite GetFrame(Sprite[] _frames)
    {
        switch (_race)
        {
            case RaceCard.Demons:
                return _frames[0];

            case RaceCard.Gods:
                return _frames[1];

            case RaceCard.Humans:
                return _frames[2];
        }

        return null;
    }
}