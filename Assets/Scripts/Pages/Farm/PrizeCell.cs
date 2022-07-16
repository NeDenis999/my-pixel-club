using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrizeCell : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _amountPrizeText;

    private int _amountPrize; 
    private PrizeType _typePrize;

    public int AmountPrize => _amountPrize;
    public PrizeType TypePrize => _typePrize;

    public void RenderGetingPrize(Prize prize)
    {
        _icon.sprite = prize.UIIcon;
        _typePrize = prize.TypePrize;

        _amountPrize = prize.AmountPrize;

        _amountPrizeText.text = $"{_amountPrize}";
    }

    public void RenderPosiblePrize(RandomPrize prize)
    {
        _icon.sprite = prize.UIIcon;
        _typePrize = prize.TypePrize;

        _amountPrizeText.text = $"{prize.MinNumberPrize} - {prize.MaxNumberPrize}";
    }
}
