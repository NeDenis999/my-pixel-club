using Cards;
using Collection;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

public class CardCollectionCell : CardCell, IInventory
{
    [SerializeField] 
    private Button _button;

    [SerializeField] 
    private ChangingCursorHover _changingCursorHover;
    
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

    public void InitBase(AttackDeck attackDeck, DefenceDeck defenceDeck, AssetProviderService assetProviderService)
    {
        _attackDeck = attackDeck;
        _defenceDeck = defenceDeck;
    }

    private void SetCardInDeck()    
    {
        if (_attackDeck.gameObject.activeSelf)
            _attackDeck.SetCardInDeck(this);
        else if (_defenceDeck.gameObject.activeSelf)
            _defenceDeck.SetCardInDeck(this);
    }
}
