using Data;
using Pages.Roulette;
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
        _cristalWallet.AddÑurrency(amountCristal);

    public void AccrueGold(int amountGold) =>
        _goldWallet.AddÑurrency(amountGold);

    private void StartSpine()
    {
        if (_cristalWallet.AmountMoney >= _spinePrise)
        {
            _numbmerPrize = RandomCell();
                
            _startRoletteButton.interactable = false;
            StartCoroutine(_rouletteAnimator.Spine(_numbmerPrize, _rouletteCells));
            _cristalWallet.WithdrawÑurrency(_spinePrise);
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

