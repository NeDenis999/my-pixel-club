using System;
using Data;
using Roulette;
using UnityEngine;
using TMPro;
using Zenject;

public abstract class Wallet : MonoBehaviour
{    
    [SerializeField] private TMP_Text _textMoney;
    [SerializeField] protected int _amountMoney;

    [SerializeField] protected Farm _farm;
    [SerializeField] protected QuestPrizeWindow _questPrizeWindow;
    [SerializeField] protected RoulettePage roulettePage;
    
    public int AmountMoney => _amountMoney;

    protected void RefreshText() => 
        _textMoney.text = _amountMoney.ToString();

    protected void WithdrawСurrency(int money)
    {
        if (money > _amountMoney)
            throw new InvalidOperationException();
        
        _amountMoney -= money;
        UpdateСurrencyText();
    }

    protected void AddСurrency(int countMoney)
    {
        _amountMoney += countMoney;
        UpdateСurrencyText();
    }

    private void UpdateСurrencyText() => 
        _textMoney.text = _amountMoney.ToString();
}
