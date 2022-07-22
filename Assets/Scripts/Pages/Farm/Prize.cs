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
    [SerializeField] private ScriptableObject _prizeAsScriptableObject;

    public virtual IPrize PrizeAsInterface => _prizeAsScriptableObject as IPrize;

    [SerializeField] protected int _minPrizeValue;
    public virtual int AmountPrize => _minPrizeValue;
    public Sprite UIIcon
    {
        get
        {
           return PrizeAsInterface.UIIcon;
        }
    }
    public void TakeItem(IIncreaserWalletValueAndCardsCount increaser)
    {
        PrizeAsInterface.TakeItemAsPrize(increaser, AmountPrize);
    }
}
