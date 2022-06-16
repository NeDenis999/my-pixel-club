using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using Zenject;

public class CristalWallet : Wallet
{
    [SerializeField] private Shop _shop;
    
    private PlayerDataScriptableObject _data;
    
    [Inject]
    public void Construct(PlayerDataScriptableObject data)
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
    
    private void OnApplicationQuit() => 
        _data.PlayerData.Crystals = _amountMoney;
}
