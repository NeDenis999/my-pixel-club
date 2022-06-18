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

    public void Render(Prize card)
    {
        _amountPrize = card.AmountPrize;
        _icon.sprite = card.Sprite;
        _typePrize = card.TypePrize;

        _amountPrizeText.text = _amountPrize.ToString();
    }
}
