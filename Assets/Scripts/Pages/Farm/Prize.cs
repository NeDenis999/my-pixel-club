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
public class Prize 
{
    public Sprite Sprite;
    public int MinNumberPrize;
    public int MaxNumberPrize;
    public int AmountPrize { get; set; }
    public PrizeType TypePrize;

    public Sprite UIIcon => Sprite;
    public string Description => TypePrize.ToString();

    //public void TakeItem()
    //{
    //    var roulettePage = FindObjectOfType<RoulettePage>().gameObject.GetComponent<RoulettePage>();

    //    if (TypePrize == PrizeType.Cristal)
    //        roulettePage.AccrueCristal();

    //    if (TypePrize == PrizeType.Gold)
    //        roulettePage.AccrueGold();
    //}
}
