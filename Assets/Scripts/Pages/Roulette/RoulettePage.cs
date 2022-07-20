using Data;
using FarmPage.Roulette;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class RoulettePage : MonoBehaviour, IIncreaserWalletValueAndCardsCount
{
    [SerializeField] private CardCollection _cardCollection;

    [SerializeField]
    private RouletteCell[] _rouletteCells;

    [SerializeField] private int _spinePrise;

    [SerializeField] private Button _startRoletteButton, _collectButton;

    [SerializeField] private RouletteAnimator _rouletteAnimator;

    [SerializeField] private CristalWallet _cristalWallet;
    [SerializeField] private GoldWallet _goldWallet;
    [SerializeField] private Inventory _inventory;

    private int _numbmerPrize;

    private void OnEnable()
    {
        _startRoletteButton.onClick.AddListener(StartSpine);
        _collectButton.onClick.AddListener(StartCloseWinningPanel);
    }

    private void OnDisable()
    {
        _startRoletteButton.onClick.RemoveListener(StartSpine);
        _collectButton.onClick.RemoveListener(StartCloseWinningPanel);
    }

    public void AccrueCard(CardData card, int count)
    {
        for (int i = 0; i < count; i++)
            _cardCollection.AddCard(card);
    }

    public void AccrueCristal(int amountCristal) =>
        _cristalWallet.Add—urrency(amountCristal);

    public void AccrueGold(int amountGold) =>
        _goldWallet.Add—urrency(amountGold);

    public void AccrueBottle(ShopItemBottle bottle, int amountBottle)
    {
        for (int i = 0; i < amountBottle; i++)
            _inventory.AddItem(bottle);
    }

    private void StartSpine()
    {
        if (_cristalWallet.AmountMoney >= _spinePrise)
        {
            _numbmerPrize = RandomCell();
                
            _startRoletteButton.interactable = false;
            StartCoroutine(_rouletteAnimator.Spine(_numbmerPrize, _rouletteCells));
            _cristalWallet.Withdraw—urrency(_spinePrise);
        }
    }
        
    private int RandomCell() => 
        Random.Range(0, _rouletteCells.Length);
    
    private void TakeItem(Prize rouletteItem)
    {
       rouletteItem.TakeItem(this);
    }

    private void StartCloseWinningPanel()
    {
        StartCoroutine(_rouletteAnimator.CloseWinningPanel(_startRoletteButton));
        TakeItem(_rouletteCells[_numbmerPrize].RouletteItem);
    }
}

