using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldWallet : Wallet
{
    [SerializeField] ConfirmWindow _confirmWindow;    

    private void OnEnable()
    {
        _confirmWindow.OnWithdrawMoney += Withdraw—urrency;
        _farm.OnAcceruGold += Add—urrency;
        _questPrizeWindow.OnAcceruGold += Add—urrency;
        _roulette.OnReceivedGold += Add—urrency;
    }    
}