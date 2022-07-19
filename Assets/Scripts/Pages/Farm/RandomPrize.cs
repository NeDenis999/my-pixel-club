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

    public RandomPrize(int minNumberPrize, int maxNumberPrize, PrizeType prizeType)
    {
        _minPrizeValue = minNumberPrize;
        _maxPrizeValue = maxNumberPrize;
        TypePrize = prizeType;
    }
}
