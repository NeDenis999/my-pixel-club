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
        //_battle.OnPlayerWin += OpenPrizeWindow;
    }

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
            _prizes.Add(AddNewPrize(prizes[i]));
        }
    }

    private PrizeCell AddNewPrize(RandomPrize prizes)
    {
        var cell = Instantiate(_prizeCellTemplate, _container);
        cell.RenderGetingPrize(prizes);
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
