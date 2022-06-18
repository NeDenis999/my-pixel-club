using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using Zenject;

public class CristalWallet : Wallet
{
    [SerializeField] private Shop _shop;

    private DataSaveLoadService _data;
    
    [Inject]
    public void Construct(DataSaveLoadService data)
    {
        _data = data;
    }
    
    private void Start()
    {
        _amountMoney = _data.PlayerData.Crystals;
        
        _shop.OnCristalBuy += AddСurrency;
        _farm.OnAcceruCristal += AddСurrency;
        _questPrizeWindow.OnAcceruCristal += AddСurrency;
        roulettePage.OnReceivedCristal += AddСurrency;
        roulettePage.OnBuyRouletteSpin += WithdrawСurrency;

        RefreshText();
    }
}
