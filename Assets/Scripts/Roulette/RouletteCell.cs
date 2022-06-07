using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteCell : MonoBehaviour
{
    [SerializeField] private Image _icon;

    [SerializeField] private ScriptableObject _rouletteItem;

    public IRoulette RouletteItem => _rouletteItem as IRoulette;

    private void OnEnable()
    {
        Render(_rouletteItem as IRoulette);
    }

    private void Render(IRoulette item)
    {
        _icon.sprite = item.UIIcon;
    }    
}
