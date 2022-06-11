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

    [SerializeField] private RoulettePage roulettePage;

    public Sprite UIIcon => Sprite;

    public void TakeItem()
    {
        if (TypePrize == PrizeType.Cristal)
            roulettePage.ReceiveCristal();

        if (TypePrize == PrizeType.Gold)
            roulettePage.ReceiveGold();
    }
}
