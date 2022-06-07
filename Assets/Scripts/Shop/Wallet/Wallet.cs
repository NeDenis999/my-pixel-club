using System;
using UnityEngine;
using TMPro;

public abstract class Wallet : MonoBehaviour
{    
    [SerializeField] private TMP_Text _textMoney;
    [SerializeField] protected int _amountMoney;

    [SerializeField] protected Farm _farm;
    [SerializeField] protected QuestPrizeWindow _questPrizeWindow;
    [SerializeField] protected Roulette _roulette;

    public int AmountMoney => _amountMoney;

    private void OnEnable()
    {
        _textMoney.text = _amountMoney.ToString();
    }

    protected void Withdraw—urrency(int money)
    {
        if (money > _amountMoney)
            throw new InvalidOperationException();
        _amountMoney -= money;
        Update—urrencyText();
    }

    protected void Add—urrency(int countMoney)
    {
        _amountMoney += countMoney;
        Update—urrencyText();
    }

    private void Update—urrencyText()
    {
        _textMoney.text = _amountMoney.ToString();
    }
   
}
