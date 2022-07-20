using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomPrize : Prize
{
    [SerializeField] private int _maxPrizeValue;
    private IPrize _prize;

    public override int AmountPrize => Random.Range(_minPrizeValue, _maxPrizeValue);
    public override IPrize PrizeAsInterface => _prize ?? base.PrizeAsInterface;
    public int MinNumberPrize => _maxPrizeValue;
    public int MaxNumberPrize => _minPrizeValue;

    public RandomPrize(int minNumberPrize, int maxNumberPrize, IPrize roulettePrize)
    {
        _minPrizeValue = minNumberPrize;
        _maxPrizeValue = maxNumberPrize;
        _prize = roulettePrize;
    }
}
