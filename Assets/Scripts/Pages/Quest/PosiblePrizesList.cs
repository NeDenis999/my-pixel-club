using Pages.Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosiblePrizesList : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private PrizeCell _prizeCellTamplate;

    [SerializeField] private Chapter _chapter;

    private void Start()
    {
        Render();
    }

    public void Render()
    {
        foreach (var posiblePrize in _chapter.PosiblePrizes)
        {
            var cell = Instantiate(_prizeCellTamplate, _container);
            cell.RenderPosiblePrize(posiblePrize);
        }
    }
}
