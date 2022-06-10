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

[CreateAssetMenu(fileName = "Farm Prize", menuName = "Farm")]
public class Prize : ScriptableObject, IRoulette
{
    public Sprite Sprite;
    public int AmountPrize;
    public PrizeType TypePrize;

    public Sprite UIIcon => Sprite;

    public void TakeItem()
    {
        var roulette = FindObjectOfType<Roulette>().gameObject.GetComponent<Roulette>();

        if (TypePrize == PrizeType.Cristal)
            roulette.ReceiveCristal();

        if (TypePrize == PrizeType.Gold)
            roulette.ReceiveGold();
    }
}
