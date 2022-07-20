using FarmPage.Farm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosiblePrizesWindow : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private PrizeCell _prizeCellTamplate;

    [SerializeField] private CooldownSelector _cooldownSelector;

    private RandomPrize[] _currentRandomPrizes;

    public RandomPrize[] CurrentRandomPrizes => _currentRandomPrizes;

    public void RenderPrize(Place place)
    {
        GeneratePrizesValue(place);

        foreach (Transform child in _container)
            Destroy(child.gameObject);

        foreach (var prize in _currentRandomPrizes)
        {
            var cell = Instantiate(_prizeCellTamplate, _container);
            cell.RenderPosiblePrize(prize);
        }
    }

    private void GeneratePrizesValue(Place place)
    {
        RandomPrize[] randomPrizes = new RandomPrize[place.Data.RandomPrizes.Length];

        for (int i = 0; i < randomPrizes.Length; i++)
            randomPrizes[i] = new RandomPrize(place.Data.RandomPrizes[i].MinNumberPrize * _cooldownSelector.PrizeMultiplyer, 
                                              place.Data.RandomPrizes[i].MaxNumberPrize * _cooldownSelector.PrizeMultiplyer, 
                                              place.Data.RandomPrizes[i].PrizeAsInterface);

        _currentRandomPrizes = randomPrizes;
    }
}
