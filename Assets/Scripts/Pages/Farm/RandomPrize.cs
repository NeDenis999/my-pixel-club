using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomPrize : Prize
{
    [SerializeField] private  int _maxPrizeValue;

    public override int AmountPrize => Random.Range(_minPrizeValue, _maxPrizeValue);
    public int MinNumberPrize => _maxPrizeValue;
    public int MaxNumberPrize => _minPrizeValue;
    public IPrize RoulettePrize => _prize;

    public RandomPrize(int minNumberPrize, int maxNumberPrize, IPrize roulettePrize)
    {
        _minPrizeValue = minNumberPrize;
        _maxPrizeValue = maxNumberPrize;
        _prize = roulettePrize;
    }
}
