using Infrastructure.Services;
using Roulette;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public enum RarityCard
{
    Epmpty,
    Standard,
    Rarity
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
    [SerializeField] private Sprite _currentImage;

    [SerializeField] private Sprite _imageFirstEvolution;
    [SerializeField] private Sprite _imageSecondeEvolution;
    private int _evolution = 1;
    public int Evoulution => _evolution;

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

    public Sprite UIIcon => _currentImage;
    public string Name => _name;

    public RarityCard Rarity => _rarity;
    public RaceCard Race => _race;

    public int Attack => _attack;
    public int Def => _def;
    public int Health => _health;
    public int Level => _level;
    public Vector2 DirectionView => _directionView;
    public ParticleSystem AttackEffect => _attackEffect;
    public Image AttackIcon => _attackIcon;

    public int BonusAttackSkill => _attackSkill;
    public void TakeDamage(int damage) => _health -= damage;

    public string AttackSkillName => _attackSillName;

    public int BonusDefSkill => _defSkill;
    public string DefSkillName => _defSkillName;

    public float SkillChance => (float)_skillChance;

    public string Discription => _discription;
    public int Id { get; set; }

    Card ICard.Card => this;

    public void Evolve(EvolutionCard firstCard, EvolutionCard secondCard)
    {
        if (firstCard.CardCell.UIIcon != secondCard.CardCell.UIIcon || _evolution == 2) throw new System.InvalidOperationException();

        float valueIncreaseMultiplier = 1.35f;

        int GetLevelUpValue(int firstValue, int secondValue)
        {
            return (int)((firstValue + secondValue) / 2 * valueIncreaseMultiplier);
        }

        _attack = GetLevelUpValue(firstCard.CardCell.Attack, secondCard.CardCell.Attack);
        _def = GetLevelUpValue(firstCard.CardCell.Def, secondCard.CardCell.Def);
        _health = GetLevelUpValue(firstCard.CardCell.Health, secondCard.CardCell.Health);

        _currentImage = _imageSecondeEvolution;
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