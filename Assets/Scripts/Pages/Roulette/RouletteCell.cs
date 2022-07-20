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
    public Prize RouletteItem => _rouletteItem;

    [SerializeField] private string _name;
    public string Name => _name;

    private void Start()
    {
        Render(_rouletteItem);
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

    private void Render(Prize prize)
    {
        _icon.sprite = prize.UIIcon;
        _amountPrize.text = prize.AmountPrize.ToString();
    }    
}
