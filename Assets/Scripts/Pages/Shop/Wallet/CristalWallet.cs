using Infrastructure.Services;
using UnityEngine;
using Zenject;

public class CristalWallet : Wallet
{
    [SerializeField] private Shop _shop;
    
    private void Start()
    {
        _amountMoney = _data.PlayerData.Crystals;
        
        _questPrizeWindow.OnAcceruCristal += AddСurrency;
        roulettePage.OnReceivedCristal += AddСurrency;
        roulettePage.OnBuyRouletteSpin += WithdrawСurrency;

        RefreshText();
    }

    public override void AddСurrency(int countMoney)
    {
        base.AddСurrency(countMoney);

        _data.SetCrystalsCount(_amountMoney);
    }

    public override void WithdrawСurrency(int money)
    {
        base.WithdrawСurrency(money);

        _data.SetCrystalsCount(_amountMoney);
    }
}
