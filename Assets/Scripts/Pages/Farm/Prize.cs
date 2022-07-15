using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PrizeType
{
    Gold,
    Cristal
}

[System.Serializable]
public class Prize : IRoulette
{
    [SerializeField] private Sprite GoldSprite, CristalSprite;
    public int MinNumberPrize;
    public int MaxNumberPrize;
    public int AmountPrize => Random.Range(MinNumberPrize, MaxNumberPrize);
    public PrizeType TypePrize;

    public Sprite UIIcon 
    {
        get 
        {
            return TypePrize == PrizeType.Gold ? GoldSprite : CristalSprite;
        }
    }
    public string Description => TypePrize.ToString();

    public void TakeItem(RoulettePage roulettePage)
    {
        if (TypePrize == PrizeType.Cristal)
            roulettePage.AccrueCristal();

        if (TypePrize == PrizeType.Gold)
            roulettePage.AccrueGold();
    }
}
