using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
{
    public event UnityAction<int> OnBuyRouletteSpin;

    public event UnityAction<Card[]> OnReceivedCard;
    public event UnityAction<int> OnReceivedCristal;
    public event UnityAction<int> OnReceivedGold;
    
    [SerializeField]
    private float _braking;
    
    [SerializeField] 
    private int _spinePrise;
    
    [SerializeField] 
    private RouletteCell[] _rouletteCells;

    [SerializeField] 
    private Button _startRoletteButton;

    [SerializeField]
    private Transform _target;
    
    [SerializeField]
    private CanvasGroup _winningPanel;

    [SerializeField] 
    private Button _collectButton;
    
    private Transform _currentParrent;
    private RouletteCell _currentCell;
    private Vector3 _previousCurrentCellPosition;
    private Vector3 _previousCurrentCellScale;

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

    public void ReceiveCard(Card card) => 
        OnReceivedCard?.Invoke(new Card[] { card });

    public void ReceiveCristal() => 
        OnReceivedCristal?.Invoke(Random.Range(1, 6));

    public void ReceiveGold() => 
        OnReceivedGold?.Invoke(Random.Range(1,6));

    private void StartSpine()
    {
        if (FindObjectOfType<CristalWallet>().gameObject.GetComponent<CristalWallet>().AmountMoney >= _spinePrise)
        {
            OnBuyRouletteSpin?.Invoke(_spinePrise);
            _startRoletteButton.interactable = false;
            UnselectedAllCells();
            StartCoroutine(Spine(RandomCell()));
        }
    }

    private int RandomCell() => 
        Random.Range(0, _rouletteCells.Length);

    private IEnumerator Spine(int prize)
    {
        float rotationSpeed = 0.1f;

        int currentCellNumber = 0;
        bool isCirclePassed = false;

        while (prize != currentCellNumber || isCirclePassed == false)
        {
            _rouletteCells[currentCellNumber].Unselect();

            if (currentCellNumber < _rouletteCells.Length - 1)
            {
                currentCellNumber++;
            }
            else
            {
                isCirclePassed = true;
                currentCellNumber = 0;
            }

            _rouletteCells[currentCellNumber].Select();

            if (rotationSpeed < _braking && isCirclePassed)
                rotationSpeed *= 1.2f;

            yield return new WaitForSeconds(rotationSpeed);
        }

        ReceiveItem(_rouletteCells[prize].RouletteItem);

        _currentCell = _rouletteCells[currentCellNumber];
        _currentParrent = _currentCell.transform.parent;

        _previousCurrentCellPosition = _currentCell.transform.localPosition;
        _previousCurrentCellScale = _currentCell.transform.localScale;
        _currentCell.transform.parent = _target;
        _currentCell.transform.DOLocalMove(Vector3.zero, 1);
        yield return new WaitForSeconds(1);
        _currentCell.transform.DOScale(new Vector3(30, 30, 1), 1);
        yield return new WaitForSeconds(1);
        DOTween.To(() => _winningPanel.alpha, x => _winningPanel.alpha = x, 1, 1);
        yield return new WaitForSeconds(1);
        _winningPanel.interactable = true;
        _winningPanel.blocksRaycasts = true;
    }

    private void UnselectedAllCells()
    {
        foreach (var rouletteCell in _rouletteCells)
            rouletteCell.Unselect();
    }

    
    private void ReceiveItem(IRoulette rouletteItem)
    {
        var taker = rouletteItem;
        taker.TakeItem();
    }

    private void StartCloseWinningPanel() => 
        StartCoroutine(CloseWinningPanel());

    private IEnumerator CloseWinningPanel()
    {
        _currentCell.transform.parent = _currentParrent;
        DOTween.To(() => _winningPanel.alpha, x => _winningPanel.alpha = x, 0, 0.75f)
            .OnComplete(() =>
            {
                _winningPanel.blocksRaycasts = false;
                _winningPanel.interactable = false;
            });
        //yield return new WaitForSeconds(0.5f);
        _currentCell.transform.DOScale(_previousCurrentCellScale, 0.75f);
        _currentCell.Unselect();
        yield return new WaitForSeconds(0.75f);
        _currentCell.transform.DOLocalMove(_previousCurrentCellPosition, 0.75f);
        yield return new WaitForSeconds(0.75f);
        _startRoletteButton.interactable = true;
    }
}
