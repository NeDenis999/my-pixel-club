using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomPrize : Prize
{
    public int MinNumberPrize;
    public int MaxNumberPrize;

    public override int AmountPrize => Random.Range(MinNumberPrize, MaxNumberPrize);
}
