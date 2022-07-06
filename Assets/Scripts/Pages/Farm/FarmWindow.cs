using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FarmWindow : MonoBehaviour
{
    [SerializeField] private string _discriptionText;
    [SerializeField] private Image _loñationImage;
    [SerializeField] private TMP_Text _locationName, _discription;    

    [SerializeField] private Farm _farm;

    [SerializeField] private PrizeCell _farmPrizeCellTemplate;
    [SerializeField] private Transform _container;

    private void OnEnable()
    {
        _discription.text = _discriptionText;
        _farm.OnAddNewPrize += AddNewPrize;
    }

    private void OnDisable()
    {
        _farm.OnAddNewPrize -= AddNewPrize;
    }

    public void GetPrizes()
    {
        _farm.AccruePrizes();
        ClearPrizeInWindow();
    }

    private PrizeCell AddNewPrize(Prize farmPrize)
    {
        var cell = Instantiate(_farmPrizeCellTemplate, _container);
        cell.Render(farmPrize);
        return cell;
    }

    private void ClearPrizeInWindow()
    {
        foreach (Transform childe in _container)
        {
            Destroy(childe.gameObject);
        }
    }
}
