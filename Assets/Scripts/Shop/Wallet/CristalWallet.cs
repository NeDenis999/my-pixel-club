using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalWallet : Wallet
{
    [SerializeField] private Shop _shop;
    [SerializeField] private RouletteArrow _rouletteArrow;

    private void Start()
    {
        _shop.OnCristalBuy += Add—urrency;
        _farm.OnAcceruCristal += Add—urrency;
        _questPrizeWindow.OnAcceruCristal += Add—urrency;
        _roulette.OnReceivedCristal += Add—urrency;
        _rouletteArrow.OnBuyRouletteSpin += Withdraw—urrency;
    }   
}
