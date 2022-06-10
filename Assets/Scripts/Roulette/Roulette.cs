using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Roulette : MonoBehaviour
{
    public event UnityAction<Card[]> OnReceivedCard;
    public event UnityAction<int> OnReceivedCristal;
    public event UnityAction<int> OnReceivedGold;
    public event UnityAction<int> OnBuyRouletteSpin;
    
    [SerializeField]
    private float _braking = 1.5f;
    
    [SerializeField] 
    private int _spinePrise;
    
    [SerializeField] 
    private float _startSpeedRotation;
    
    [SerializeField] 
    private RouletteCell[] _rouletteCells;

    [SerializeField] 
    private Button _startRoletteButton;
    
    private float _currentSpeedRotation;

    private void OnEnable()
    {
        _startRoletteButton.onClick.AddListener(StartSpine);
    }

    private void OnDisable()
    {
        _startRoletteButton.onClick.RemoveListener(StartSpine);
    }

    public void ReceiveCard(Card card) => 
        OnReceivedCard?.Invoke(new Card[] { card });

    public void ReceiveCristal() => 
        OnReceivedCristal?.Invoke(Random.Range(1, 6));

    public void ReceiveGold() => 
        OnReceivedGold?.Invoke(Random.Range(1,6));

    public void StartSpine()
    {
        if (FindObjectOfType<CristalWallet>().gameObject.GetComponent<CristalWallet>().AmountMoney >= _spinePrise)
        {
            OnBuyRouletteSpin?.Invoke(_spinePrise);
            _currentSpeedRotation = _startSpeedRotation;
            _startRoletteButton.interactable = false;
            StartCoroutine(Spine(RandomCell()));
        }
    }

    private IEnumerator Spine(int price)
    {
        var currentCell = 0;
        var isCirclePassed = false;

        while (price != currentCell || !isCirclePassed)
        {
            if (currentCell < _rouletteCells.Length - 1)
                currentCell++;
            else
            {
                isCirclePassed = true;
                currentCell = 0;
            }

            UnselectedAllCells();
            _rouletteCells[currentCell].Select();

            if (_currentSpeedRotation < _braking && isCirclePassed)
                _currentSpeedRotation *= 1.2f;

            yield return new WaitForSeconds(_currentSpeedRotation);
        }

        ReceiveItem(_rouletteCells[price].RouletteItem);
        _startRoletteButton.interactable = true;
    }

    private void UnselectedAllCells()
    {
        foreach (var rouletteCell in _rouletteCells)
            rouletteCell.Unselect();
    }

    private int RandomCell() => 
        Random.Range(0, _rouletteCells.Length);
    
    private void ReceiveItem(IRoulette rouletteItem)
    {
        var taker = rouletteItem;
        taker.TakeItem();
    }
}
