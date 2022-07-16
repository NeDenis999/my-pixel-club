using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouletteCell : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _amountPrize;

    [SerializeField] private Prize _rouletteItem;
    
    public IRoulette RouletteItem => _rouletteItem;

    private void Start()
    {
        Render(RouletteItem);
        Unselect();
    }

    public void Select()
    {
        //_image.color.SetAlpha(1);
        _icon.color = new Color(1, 1, 1, 1);
    }

    public void Unselect()
    {
        //_image.color.SetAlpha(0.75f);
        _icon.color = new Color(0.5f, 0.5f, 0.5f,1);
    }

    private void Render(IRoulette item)
    {
        _icon.sprite = item.UIIcon;
        if(item is Prize)
            _amountPrize.text = (item as Prize).AmountPrize.ToString();
    }    
}
