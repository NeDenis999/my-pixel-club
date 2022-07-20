using TMPro;
using System.Collections.Generic;
using Battle;
using FarmPage.Battle;
using FarmPage.Quest;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Data;

public class QuestPrizeWindow : MonoBehaviour, IIncreaserWalletValueAndCardsCount
{
    [SerializeField] private PrizeCell _prizeCellTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private BattleController _battle;
    [SerializeField] private Button _collectButton;

    [SerializeField] private CristalWallet _cristalWallet;
    [SerializeField] private GoldWallet _goldWallet;
    
    
    private List<PrizeCell> _prizeCells = new();

    private void OnEnable()
    {
        _collectButton.onClick.AddListener(AccruePrizes);
    }

    private void OnDisable()
    {
        foreach (Transform child in _container)
            Destroy(child.gameObject);

        _collectButton.onClick.RemoveListener(AccruePrizes);
    }

    public void OpenPrizeWindow(RandomPrize[] prizes)
    {
        if (prizes == null) throw new System.NullReferenceException();

        gameObject.SetActive(true);
        GeneratePrizes(prizes);
    }

    private void GeneratePrizes(RandomPrize[] prizes)
    {
        for (int i = 0; i < prizes.Length; i++)
        {
            _prizeCells.Add(CreateNewPrize(prizes[i]));
        }
    }

    private PrizeCell CreateNewPrize(RandomPrize prizes)
    {
        var cell = Instantiate(_prizeCellTemplate, _container);
        cell.RenderGetingPrize(prizes);
        return cell;
    }

    private void AccruePrizes()
    {
        foreach (var prizeCell in _prizeCells)
        {
            prizeCell.Prize.TakeItemAsPrize(this, prizeCell.AmountPrize);
        }

        _prizeCells.Clear();
    }

    public void AccrueCard(CardData card, int count)
    {
        throw new System.NotImplementedException();
    }

    public void AccrueCristal(int amountCristal) =>
        _cristalWallet.Add—urrency(amountCristal);

    public void AccrueGold(int amountGold) =>
        _goldWallet.Add—urrency(amountGold);

    public void AccrueBottle(ShopItemBottle bottle, int amountBottle)
    {
        throw new System.NotImplementedException();
    }
}
