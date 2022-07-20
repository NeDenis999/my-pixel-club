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
    public int AmountPrize => _amountPrize;
    public IPrize Prize { get; private set; }


    public void RenderGetingPrize(Prize prize)
    {
        Prize = prize.PrizeAsInterface;

        _icon.sprite = prize.UIIcon;

        _amountPrize = prize.AmountPrize;

        _amountPrizeText.text = $"{_amountPrize}";
    }

    public void RenderPosiblePrize(RandomPrize prize)
    {
        Prize = prize.PrizeAsInterface;

        _icon.sprite = prize.UIIcon;

        _amountPrizeText.text = $"{prize.MinNumberPrize} - {prize.MaxNumberPrize}";
    }
}
