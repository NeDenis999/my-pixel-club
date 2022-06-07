using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestPrizeWindow : MonoBehaviour
{
    public event UnityAction<int> OnAcceruGold;
    public event UnityAction<int> OnAcceruCristal;

    [SerializeField] private List<Prize> _variationPrizes;
    [SerializeField] private PrizeCell _farmPrizeCellTemplate;
    [SerializeField] private Transform _container;

    [SerializeField] private QuestFight _quest;
    [SerializeField] private Battle _battle;

    [SerializeField] private TMP_Text _winOrLoseText;

    private List<PrizeCell> _prizes = new();

    private void Start()
    {
        _quest.OnPlayerWin += OpenPrizeWindow;
        _battle.OnPlayerWin += OpenPrizeWindow;

        _quest.OnPlayerLose += OpenLoseWindow;
        _battle.OnPlayerLose += OpenLoseWindow;

        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        foreach (Transform child in _container)
            Destroy(child.gameObject);
    }

    private void OpenLoseWindow()
    {
        gameObject.SetActive(true);
        _winOrLoseText.text = "You lose!";
    }

    private void OpenPrizeWindow()
    {
        gameObject.SetActive(true);
        GeneratePrizes();
        AccruePrizes();
        _winOrLoseText.text = @"YOU WIN 
You have received";
    }

    private void GeneratePrizes()
    {
        for (int i = 0; i < 3; i++)
        {
            _prizes.Add(AddNewPrize());
        }
    }

    private PrizeCell AddNewPrize()
    {
        var cell = Instantiate(_farmPrizeCellTemplate, _container);
        cell.Render(GetRandomPrize());
        return cell;
    }

    private Prize GetRandomPrize()
    {
        var prize = _variationPrizes[Random.Range(0, _variationPrizes.Count)];
        prize.AmountPrize = GetRandomPrizeValue();
        return prize;
    }

    private int GetRandomPrizeValue()
    {
        return Random.Range(1, 8);
    }

    private void AccruePrizes()
    {
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
    }
}
