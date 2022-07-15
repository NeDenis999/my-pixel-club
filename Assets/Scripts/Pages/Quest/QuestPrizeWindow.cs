using TMPro;
using System.Collections.Generic;
using Battle;
using Pages.Battle;
using Pages.Quest;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestPrizeWindow : MonoBehaviour
{
    public event UnityAction<int> OnAcceruGold;
    public event UnityAction<int> OnAcceruCristal;

    [SerializeField] private List<Prize> _variationPrizes;
    [SerializeField] private PrizeCell _prizeCellTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private BattleController _battle;
    [SerializeField] private Button _collectButton;
    
    
    private List<PrizeCell> _prizes = new();

    private void Start()
    {
        _battle.OnPlayerWin += OpenPrizeWindow;
    }

    private void OnEnable()
    {
        _collectButton.onClick.AddListener(AccruePrizes);
    }

    public void OpenPrizeWindow()
    {
        gameObject.SetActive(true);
        GeneratePrizes();
    }

    private void OnDisable()
    {
        foreach (Transform child in _container)
            Destroy(child.gameObject);

        _collectButton.onClick.RemoveListener(AccruePrizes);
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
        var cell = Instantiate(_prizeCellTemplate, _container);
        cell.RenderGetingPrize(_variationPrizes[Random.Range(0, _variationPrizes.Count)]);
        return cell;
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
