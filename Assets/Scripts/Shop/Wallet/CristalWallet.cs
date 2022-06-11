using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalWallet : Wallet
{
    [SerializeField] private Shop _shop;

    private void Start()
    {
        _shop.OnCristalBuy += AddСurrency;
        _farm.OnAcceruCristal += AddСurrency;
        _questPrizeWindow.OnAcceruCristal += AddСurrency;
        roulettePage.OnReceivedCristal += AddСurrency;
        roulettePage.OnBuyRouletteSpin += WithdrawСurrency;
    }   
}
