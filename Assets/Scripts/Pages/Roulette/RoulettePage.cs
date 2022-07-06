using Data;
using Pages.Roulette;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class RoulettePage : MonoBehaviour
{
    public event UnityAction<int> OnReceivedCristal;
    public event UnityAction<int> OnReceivedGold;
    public event UnityAction<int> OnBuyRouletteSpin;

    [SerializeField] private CardCollection _cardCollection;

    [SerializeField]
    private RouletteCell[] _rouletteCells;

    [SerializeField]
    private int _spinePrise;

    [SerializeField]
    private Button _startRoletteButton;

    [SerializeField]
    private Button _collectButton;

    [SerializeField]
    private RouletteAnimator _rouletteAnimator;

    [SerializeField]
    private CristalWallet _cristalWallet;

    private int _prize;

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

    public void AccrueCard(CardData card)
    {
        _cardCollection.AddCard(card);
    }

    public void AccrueCristal() => 
        OnReceivedCristal?.Invoke(Random.Range(1, 6));

    public void AccrueGold() => 
        OnReceivedGold?.Invoke(Random.Range(1,6));

    private void StartSpine()
    {
        if (_cristalWallet.AmountMoney >= _spinePrise)
        {
            _prize = RandomCell();
                
            _startRoletteButton.interactable = false;
            StartCoroutine(_rouletteAnimator.Spine(_prize, _rouletteCells));
            OnBuyRouletteSpin?.Invoke(_spinePrise);
        }
    }
        
    private int RandomCell() => 
        Random.Range(0, _rouletteCells.Length);
    
    private void TakeItem(IRoulette rouletteItem)
    {
        var taker = rouletteItem;
        taker.TakeItem();
    }

    private void StartCloseWinningPanel()
    {
        StartCoroutine(_rouletteAnimator.CloseWinningPanel(_startRoletteButton));
        TakeItem(_rouletteCells[_prize].RouletteItem);
    }
}

