using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardCollectionSort<T> : MonoBehaviour where T : CardCell
{
    protected List<T> _cards = new();

    public void AttackSort()
    {
        _cards = _cards.OrderByDescending(e => e.Attack).ToList();
        RenderCardsSiblingIndex();
    }

    public void DefSort()
    {
        _cards = _cards.OrderByDescending(e => e.Def).ToList();
        RenderCardsSiblingIndex();
    }

    public void GodsSort()
    {
        RaceSort(RaceCard.Gods);
    }

    public void HumansSort()
    {
        RaceSort(RaceCard.Humans);
    }

    public void DemonsSort()
    {
        RaceSort(RaceCard.Demons);
    }

    private void RaceSort(RaceCard race)
    {
        foreach (var cardCell in _cards)
        {
            cardCell.gameObject.SetActive(false);
            if (cardCell.Card.Race == race)
                cardCell.gameObject.SetActive(true);
        }
    }

    public void AllCard()
    {
        foreach (var cardCell in _cards)
            cardCell.gameObject.SetActive(true);
    }

    public void StandartRarity()
    {
        RaritySort(RarityCard.Standart);
    }

    public void RareRarity()
    {
        RaritySort(RarityCard.Rare);
    }

    public void HightRareRarity()
    {
        RaritySort(RarityCard.HightRare);
    }

    public void EpicRarity()
    {
        RaritySort(RarityCard.Epic);
    }

    public void LegendaryRarity()
    {
        RaritySort(RarityCard.Legendary);
    }

    private void RaritySort(RarityCard rarity)
    {
        foreach (var cardCell in _cards)
        {
            cardCell.gameObject.SetActive(false);
            if (cardCell.Card.Rarity == rarity)
                cardCell.gameObject.SetActive(true);
        }
    }

    protected void RenderCardsSiblingIndex()
    {
        for (int i = 0; i < _cards.Count; i++)
            _cards[i].transform.SetSiblingIndex(i);
    }
}
