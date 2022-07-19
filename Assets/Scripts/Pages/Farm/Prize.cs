using Infrastructure.Services;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public enum PrizeType
{
    Gold,
    Cristal
}

[System.Serializable]
public class Prize : IRoulette
{
    [Inject]
    private void Construct(AssetProviderService assetProviderService)
    {
        _goldSprite = assetProviderService.GoldSprite;
        _cristalSprite = assetProviderService.CristalSprite;
    }

    [SerializeField] protected int _minPrizeValue;
    public virtual int AmountPrize => _minPrizeValue;

    public PrizeType TypePrize;

    private Sprite _goldSprite, _cristalSprite;

    public Sprite UIIcon 
    {
        get 
        {
            return TypePrize == PrizeType.Gold ? _goldSprite : _cristalSprite;
        }
    }
    public string Description => TypePrize.ToString();

    public void TakeItem(RoulettePage roulettePage)
    {
        if (TypePrize == PrizeType.Cristal)
            roulettePage.AccrueCristal(AmountPrize);

        if (TypePrize == PrizeType.Gold)
            roulettePage.AccrueGold(AmountPrize);
    }
}
