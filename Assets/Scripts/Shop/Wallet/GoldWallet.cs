using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using Zenject;

public class GoldWallet : Wallet
{
    [SerializeField] ConfirmWindow _confirmWindow;    

    private PlayerDataScriptableObject _data;
    
    [Inject]
    public void Construct(PlayerDataScriptableObject data)
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

    private void OnApplicationQuit() => 
        _data.PlayerData.Coins = _amountMoney;
}