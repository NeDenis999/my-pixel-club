using Infrastructure.Services;
using UnityEngine;
using Zenject;

public class CristalWallet : Wallet
{
    [SerializeField] private Shop _shop;
    
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

    protected override void AddСurrency(int countMoney)
    {
        base.AddСurrency(countMoney);

        _data.SetCrystalsCount(_amountMoney);
    }

    protected override void WithdrawСurrency(int money)
    {
        base.WithdrawСurrency(money);

        _data.SetCrystalsCount(_amountMoney);
    }
}
