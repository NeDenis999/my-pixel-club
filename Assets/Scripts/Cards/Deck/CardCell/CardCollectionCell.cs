using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCollectionCell : CardCell, IInventory
{
    [SerializeField] private LinkBetweenCardsAndCollections _linkBetweenCardCollectionAndDeck;

    public Sprite UIIcon => Card.UIIcon;

    public string Statistic => "Name: " + Card.Name + "\nLevel: " + Level + "\nAtk: " + Attack.ToString() + "\nDef: " + Def + "\nRace: " + Card.Race + "\nSkill: " + Card.AttackSkillName + "\nSkill chance: " + Card.SkillChance + " %";
    public string Discription => Card.Discription;

    public BottleEffects Effect => BottleEffects.None;


    private void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(SetCardInDeck);
    }

    private void SetCardInDeck()
    {
        _linkBetweenCardCollectionAndDeck.SelectCard(this);
    }
}
