using Infrastructure.Services;
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
    [SerializeField] private ScriptableObject _prizeScriptableObject;

    protected IPrize _prize;

    [SerializeField] protected int _minPrizeValue;
    public virtual int AmountPrize => _minPrizeValue;
    public Sprite UIIcon
    { 
        get
        {
            if (_prizeScriptableObject is not IPrize) throw new System.InvalidOperationException("ScriptableObject is not realize IPrize");
            _prize = (_prizeScriptableObject as IPrize);

            return _prize.UIIcon;
        }
    }

    public void TakeItem(IIncreaserWalletValueAndCardsCount increaser)
    {
        if (_prize is not IPrize) throw new System.InvalidOperationException("ScriptableObject is not realize IPrize");
        _prize = (_prizeScriptableObject as IPrize);

        _prize.TakeItem(increaser, AmountPrize);
    }
}
