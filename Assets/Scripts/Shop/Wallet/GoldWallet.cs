using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldWallet : Wallet
{
    [SerializeField] ConfirmWindow _confirmWindow;    

    private void OnEnable()
    {
        _confirmWindow.OnWithdrawMoney += WithdrawСurrency;
        _farm.OnAcceruGold += AddСurrency;
        _questPrizeWindow.OnAcceruGold += AddСurrency;
        roulettePage.OnReceivedGold += AddСurrency;
    }    
}