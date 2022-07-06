using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Farm : MonoBehaviour
{
    public event System.Func<Prize, PrizeCell> OnAddNewPrize;

    public event UnityAction<int> OnAcceruGold;
    public event UnityAction<int> OnAcceruCristal;

    [SerializeField] private List<Prize> _variationFarmPrizes;

    [SerializeField] private TMP_Text _timer;

    [SerializeField] private GameObject _getButton, _startButton, _chooseCharacterButton;

    [SerializeField] private Image _nftImage;

    private bool _isNFTSet;
    private bool _isFarm;

    private List<PrizeCell> _prizes = new();

    public void SetNFT(NFT nft)
    {
        _nftImage.sprite = nft.Sprite;
        _isNFTSet = true;
        _startButton.SetActive(true);
    }

    public void StartFarm()
    {
        if (_isFarm == false && _isNFTSet)
        {
            _startButton.SetActive(false);
            _chooseCharacterButton.SetActive(false);
            StartCoroutine(GetPrizes());
            StartCoroutine(Timer());
        }
    }

    public void AccruePrizes()
    {
        if (_prizes.Count != 10) throw new System.InvalidOperationException();

        foreach (var prize in _prizes)
        {
            switch (prize.TypePrize)
            {
                case PrizeType.Gold:
                    OnAcceruGold?.Invoke(prize.AmountPrize);
                    break;
                case PrizeType.Cristal:
                    OnAcceruCristal?.Invoke(prize.AmountPrize);
                    break;
                default:
                    throw new System.ArgumentException();
            }
        }

        _prizes.Clear();

        _getButton.SetActive(false);
        _startButton.SetActive(true);
    }

    private IEnumerator GetPrizes()
    {
        _isFarm = true;

        yield return new WaitForSeconds(2);
        _prizes.Add(OnAddNewPrize?.Invoke(GetRandomPrize()));

        while (_prizes.Count < 10)
        {
            yield return new WaitForSeconds(30);

            _prizes.Add(OnAddNewPrize?.Invoke(GetRandomPrize()));
        }

        _getButton.SetActive(true);
        _chooseCharacterButton.SetActive(true);

        _isFarm = false;
    }

    private IEnumerator Timer()
    {
        _timer.text = ("Left 242 seconds");
        int maxSeconds = 272;

        while (maxSeconds > 0)
        {
            yield return new WaitForSeconds(1);
            _timer.text = ("Left: " + ((maxSeconds -= 1).ToString() + " seconds"));
        }
    }

    private Prize GetRandomPrize()
    {
        var farmPrize = _variationFarmPrizes[Random.Range(0, _variationFarmPrizes.Count)];
        farmPrize.AmountPrize = GetRandomPrizeValue();
        return farmPrize;
    }

    private int GetRandomPrizeValue()
    {
        return Random.Range(1, 8);
    }
}
