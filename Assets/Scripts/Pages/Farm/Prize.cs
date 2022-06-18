using System.Collections;
using System.Collections.Generic;
using Roulette;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PrizeType
{
    Gold,
    Cristal
}

[CreateAssetMenu(fileName = "Farm Prize", menuName = "Farm")]
public class Prize : ScriptableObject, IRoulette
{
    public Sprite Sprite;
    public int AmountPrize;
    public PrizeType TypePrize;

    public Sprite UIIcon => Sprite;

    public void TakeItem()
    {
        var roulettePage = FindObjectOfType<RoulettePage>().gameObject.GetComponent<RoulettePage>();

        if (TypePrize == PrizeType.Cristal)
            roulettePage.AccrueCristal();

        if (TypePrize == PrizeType.Gold)
            roulettePage.AccrueGold();
    }
}
