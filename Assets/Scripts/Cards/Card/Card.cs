using Roulette;
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

    Card ICard.Card => this;

    public void SetEvolutionValue(int attack, int def, int health)
    {
        if (_currentImage.name == _imageSecondeEvolution.name) throw new System.InvalidOperationException();
        _attack = attack;
        _def = def;
        _health = health;
        _currentImage = _imageSecondeEvolution;
        _evolution++;
        if (_evolution > 2) throw new System.InvalidOperationException();
    }

    public void TakeItem()
    {
        var roulettePage = FindObjectOfType<RoulettePage>().gameObject.GetComponent<RoulettePage>();

        roulettePage.AccrueCard(this);
    }
}