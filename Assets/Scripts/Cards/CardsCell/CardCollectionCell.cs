using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCollectionCell : CardCell, IInventory
{
    private AttackDeck _attackDeck;
    private DefDeck _defDeck;

    public Sprite UIIcon => Card.UIIcon;

    public string Statistic => "Name: " + Card.Name + "\nLevel: " + Level + "\nAtk: " + Attack.ToString() + "\nDef: " + Def + "\nRace: " + Card.Race + "\nSkill: " + Card.AttackSkillName + "\nSkill chance: " + Card.SkillChance + " %";
    public string Discription => Card.Discription;

    public BottleEffects Effect => BottleEffects.None;


    virtual protected void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(SetCardInDeck);

        _attackDeck = FindObjectOfType<AttackDeck>().gameObject.GetComponent<AttackDeck>();
      //  _defDeck = FindObjectOfType<DefDeck>().gameObject.GetComponent<DefDeck>();
    }

    private void SetCardInDeck()    
    {
        if (_attackDeck.WritenDeck == AtackOrDefCardType.Atack)
            _attackDeck.SetCardInDeck(this);
        else
            _defDeck.SetCardInDeck(this);
    }
}
