using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

public class GoldWallet : Wallet
{
    [SerializeField] ConfirmWindow _confirmWindow;        
    
    private void OnEnable()
    {
        _amountMoney = _data.PlayerData.Coins;

        _confirmWindow.OnWithdrawMoney += WithdrawСurrency;
        _farm.OnAcceruGold += AddСurrency;
        _questPrizeWindow.OnAcceruGold += AddСurrency;
        roulettePage.OnReceivedGold += AddСurrency;
        
        RefreshText();
    }

    protected override void AddСurrency(int countMoney)
    {
        base.AddСurrency(countMoney);

        _data.SetCoinCount(_amountMoney);
    }

    protected override void WithdrawСurrency(int money)
    {
        base.WithdrawСurrency(money);

        _data.SetCoinCount(_amountMoney);
    }
}