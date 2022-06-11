using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteCell : MonoBehaviour
{
    [SerializeField] private Image _icon;

    [SerializeField] private ScriptableObject _rouletteItem;
    
    public IRoulette RouletteItem => _rouletteItem as IRoulette;

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
    }    
}
