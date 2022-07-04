using UnityEngine;
using UnityEngine.UI;

public class CardCollectionCell : CardCell, IInventory
{
    [SerializeField] 
    private Button _button;
    
    private AttackDeck _attackDeck;
    private DefenceDeck _defenceDeck;

    public Sprite UIIcon => Card.UIIcon;

    public string Statistic => "Name: " + Card.Name + "\nLevel: " + Level + "\nAtk: " + Attack.ToString() + "\nDef: " + Def + "\nRace: " + Card.Race + "\nSkill: " + Card.AttackSkillName + "\nSkill chance: " + Card.SkillChance + " %";
    public string Discription => Card.Discription;

    public BottleEffects Effect => BottleEffects.None;
    
    protected virtual void Awake()
    {
        _button.onClick.AddListener(SetCardInDeck);
    }

    public void InitBase(AttackDeck attackDeck, DefenceDeck defenceDeck)
    {
        _attackDeck = attackDeck;
        _defenceDeck = defenceDeck;
    }

    private void SetCardInDeck()    
    {
        print(_attackDeck);
        
        if (_attackDeck.WritenDeck == AtackOrDefCardType.Atack)
            _attackDeck.SetCardInDeck(this);
        else
            _defenceDeck.SetCardInDeck(this);
    }
}
