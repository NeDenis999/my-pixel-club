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

    private DataSaveLoadService _data;
    
    [Inject]
    public void Construct(DataSaveLoadService data)
    {
        _data = data;
    }
    
    private void OnEnable()
    {
        _amountMoney = _data.PlayerData.Coins;

        _confirmWindow.OnWithdrawMoney += WithdrawСurrency;
        _farm.OnAcceruGold += AddСurrency;
        _questPrizeWindow.OnAcceruGold += AddСurrency;
        roulettePage.OnReceivedGold += AddСurrency;
        
        RefreshText();
    }
}